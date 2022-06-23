using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class LoadScreen
{
    private static CanvasGroup _canvasGroup;

    public static void FadeIn(float duration)
    {
        if (_canvasGroup == null) GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<CanvasGroup>();
        _canvasGroup.DOFade(1, duration);
    }

    public static void FadeOut(float duration)
    {
        if (_canvasGroup == null) GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<CanvasGroup>();
        _canvasGroup.DOFade(0, duration);
    }
}
