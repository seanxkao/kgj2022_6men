using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ImageLayerBehaviour : PlayableBehaviour
{
    public Sprite _sprite;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var image = playerData as Image;
        if(image != null)
        {
            image.sprite = _sprite;
        }
    }
}
