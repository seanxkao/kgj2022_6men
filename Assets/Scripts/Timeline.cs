using System;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class Timeline : MonoBehaviour, INotificationReceiver
{
    [SerializeField]
    private PlayableDirector _playable;

    public PlayableDirector Playable
        => _playable;

    public event Action<DialogEvent> OnDialog;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DialogEvent dialogEvent)
        {
            OnDialog?.Invoke(dialogEvent);
        }
    }
}
