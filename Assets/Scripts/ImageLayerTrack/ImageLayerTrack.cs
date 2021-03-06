using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(ImageLayerAsset))]
[TrackBindingType(typeof(Image))]
public class ImageLayerTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<ImageLayerMixerBehaviour>.Create(graph, inputCount);
    }
}
