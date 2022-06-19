using System;
using System.Threading.Tasks;
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

    private Player player = null;
    Queue<Func<Task>> tasks = new Queue<Func<Task>>();
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

    private void Start()
    {
        player = FindObjectOfType<Player>();

        Fuck();
    }

    async Task Fuck()
    {
        while (true)
        {
            if(tasks.Count > 0)
            {
                var task = tasks.Dequeue();
                await task();
            }
            else
            {
                await Task.Yield();
            }
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
        if(player != null)
            player.canMove = false;
        foreach(var timeline in timelines)
        {
            yield return _PlayScenario(timeline);
        }
        if(player != null)
            player.canMove = true;
    }

    private IEnumerator _PlayScenario(Timeline timeline)
    {
        bool isFinished = false;
        Action<DialogEvent> onDialog = dialog =>
        {
            timeline.Playable.playableGraph.GetRootPlayable(0).SetSpeed(0f);
            tasks.Enqueue(() => _PlayDialog(dialog.Name, dialog.Text));
        };

        Action onClick = () =>
        {
            interactable = false;
            _nameView.text = string.Empty;
            _textView.text = string.Empty;
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

    private async Task _PlayDialog(string name, string text)
    {
        _nameView.text = name;
        _textView.text = string.Empty;
        await _PlayText(text);
        _textView.text = text;
        interactable = true;
    }

    private async Task _PlayText(string text)
    {
        var textPlayer = new TextPlayer(text);
        float length = textPlayer.Length;
        await Interpolation.Play(0f, length, length / _playSpeed, t =>
        {
            _textView.text = textPlayer.GetText(Mathf.FloorToInt(t));
        });
    }
}
