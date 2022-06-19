using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CanvasBehaviour : PlayableBehaviour
{
    public float alpha;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var image = playerData as CanvasGroup;
        if(image != null)
        {
            image.alpha = alpha;
        }
    }
}
