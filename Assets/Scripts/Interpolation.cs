using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public static class Interpolation
{
    public static async Task Play(float src, float dst, float duration, Action<float> onUpdate)
    {
        float t = 0f;
        while (t < duration)
        {
            onUpdate(Mathf.Lerp(src, dst, t/duration));
            t += Time.deltaTime;
            await Task.Yield();
        }
        onUpdate(dst);
    }
}