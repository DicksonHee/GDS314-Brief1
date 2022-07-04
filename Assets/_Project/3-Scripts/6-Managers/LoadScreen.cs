using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public static LoadScreen current;
    public CanvasGroup _canvasGroup;
    public Camera _loadScreenCamera;

    private void Awake()
    {
        current = this;
    }

    public void FadeIn(float duration)
    {
        _canvasGroup.alpha = 1;
        _loadScreenCamera.enabled = true;
    }

    public void FadeOut(float duration)
    {
        _canvasGroup.alpha = 0;
        _loadScreenCamera.enabled = false;
    }
}
