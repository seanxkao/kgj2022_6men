using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CanvasMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var canvasGroup = playerData as CanvasGroup;
        float alpha = 0f;

        if(canvasGroup == null)
        {
            return;
        }

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++)
        {

            float inputWeight = playable.GetInputWeight(i);
            var inputPlayable = (ScriptPlayable<CanvasBehaviour>)playable.GetInput(i);
            var input = inputPlayable.GetBehaviour();
            alpha += input.alpha * inputWeight;
        }

        canvasGroup.alpha = alpha;
        canvasGroup.interactable = alpha != 0f;
        canvasGroup.blocksRaycasts = alpha != 0f;
    }
}
