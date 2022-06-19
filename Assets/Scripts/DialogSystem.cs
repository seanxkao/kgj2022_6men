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
    private Text _nameView;
    [SerializeField]
    private Text _textView;

    [SerializeField]
    private float _playSpeed;


    bool interactable = false;
    event Action OnClick;
    
    private void Awake()
    {
        if(_playButton != null)
        {
            _playButton.onClick.AddListener(() =>
            {
                StartCoroutine(Play(_timelines));
            });
        }
    }

    private void Update()
    {
        if(interactable && Input.GetKeyDown(KeyCode.Space))
        {
            OnClick?.Invoke();
        }
    }

    public IEnumerator Play()
    {
        return Play(_timelines);
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
            _nameView.text = dialog.Name;
            StartCoroutine(_PlayDialog(dialog.Text));
        };

        Action onClick = () =>
        {
            interactable = false;
            timeline.Playable.playableGraph.GetRootPlayable(0).SetSpeed(1f);
        };

        Action<PlayableDirector> onFinished = _ => isFinished = true;

        timeline.Playable.stopped += onFinished;
        timeline.OnDialog += onDialog;
        OnClick += onClick;

        timeline.Playable.Play();
        while (!isFinished)
        {
            yield return null;
        }

        timeline.Playable.stopped -= onFinished;
        timeline.OnDialog -= onDialog;
        OnClick -= onClick;
    }

    private IEnumerator _PlayDialog(string text)
    {
        yield return _PlayText(text);
        _textView.text = text;
        interactable = true;
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
