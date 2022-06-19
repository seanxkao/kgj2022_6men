using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(CanvasAsset))]
[TrackBindingType(typeof(CanvasGroup))]
public class CanvasTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CanvasMixerBehaviour>.Create(graph, inputCount);
    }
}
