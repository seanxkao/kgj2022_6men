using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ImageLayerMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var image = playerData as Image;
        Color color = Color.clear;
        Sprite sprite = null;

        if (image == null)
        {
            return;
        }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {

            float inputWeight = playable.GetInputWeight(i);
            var inputPlayable = (ScriptPlayable<ImageLayerBehaviour>)playable.GetInput(i);
            var input = inputPlayable.GetBehaviour();
            color += input._color * inputWeight;
            sprite = input._sprite;
        }
        image.sprite = sprite;
        image.color = color;
    }
}
