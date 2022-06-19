using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Interpolation
{
    public static IEnumerator Play(float src, float dst, float duration, Action<float> onUpdate)
    {
        float t = 0f;
        while (t < duration)
        {
            onUpdate(Mathf.Lerp(src, dst, t/duration));
            t += Time.deltaTime;
            yield return null;
        }
        onUpdate(dst);
    }
}