using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogEvent : Marker, INotification
{
    public PropertyName id 
        => string.Empty;

    public string Text;
}
