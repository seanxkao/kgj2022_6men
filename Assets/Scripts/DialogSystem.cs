using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _mainPanel;
    [SerializeField]
    private Timeline[] _timelines;
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private CanvasGroup _overPanel;
    [SerializeField]
    private Button _over;
    [SerializeField]
    private Text _textView;
    [SerializeField]
    private float _playSpeed;
    
    private void Awake()
    {
        _playButton.onClick.AddListener(() => 
        {
            StartCoroutine(Play(_timelines));
        });
    }

    public IEnumerator Play(Timeline[] timelines)
    {
        foreach(var timeline in timelines)
        {
            yield return _PlayScenario(timeline);
        }
    }

    private IEnumerator _PlayScenario(Timeline timeline)
    {
        bool isFinished = false;
        Action<DialogEvent> onDialog = dialog =>
        {
            timeline.Playable.playableGraph.GetRootPlayable(0).SetSpeed(0f);
            StartCoroutine(_PlayDialog(dialog.Text));
        };

        UnityAction onClick = new UnityAction(() =>
        {
            _overPanel.blocksRaycasts = false;
            _overPanel.interactable = false;
            timeline.Playable.playableGraph.GetRootPlayable(0).SetSpeed(1f);
        });

        Action<PlayableDirector> onFinished = _ => isFinished = true;

        timeline.Playable.stopped += onFinished;
        timeline.OnDialog += onDialog;
        _over.onClick.AddListener(onClick);

        timeline.Playable.Play();
        while (!isFinished)
        {
            yield return null;
        }

        timeline.Playable.stopped -= onFinished;
        timeline.OnDialog -= onDialog;
        _over.onClick.RemoveListener(onClick);
    }

    private IEnumerator _PlayDialog(string text)
    {
        yield return _PlayText(text);
        _textView.text = text;
        _overPanel.blocksRaycasts = true;
        _overPanel.interactable = true;
    }

    private IEnumerator _PlayText(string text)
    {
        var textPlayer = new TextPlayer(text);
        float length = textPlayer.Length;
        yield return Interpolation.Play(0f, length, length / _playSpeed, t =>
        {
            _textView.text = textPlayer.GetText(Mathf.FloorToInt(t));
        });
    }
}
