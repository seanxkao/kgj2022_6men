using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ImageLayerAsset : PlayableAsset
{
    public Sprite _sprite;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {        
        var playable = ScriptPlayable<ImageLayerBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour._sprite = _sprite;
        return playable;
    }
}
