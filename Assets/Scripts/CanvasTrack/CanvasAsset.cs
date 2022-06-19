using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CanvasAsset : PlayableAsset
{
    public float alpha;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CanvasBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.alpha = alpha;
        return playable;
    }
}
