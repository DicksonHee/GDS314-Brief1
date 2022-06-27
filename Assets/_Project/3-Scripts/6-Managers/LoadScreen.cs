using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class LoadScreen
{
    private static CanvasGroup _canvasGroup;
    private static Camera _loadScreenCamera;
    
    public static void FadeIn(float duration)
    {
        if (_canvasGroup == null) _canvasGroup = GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<CanvasGroup>();
        if(_loadScreenCamera == null) _loadScreenCamera = GameObject.FindGameObjectWithTag("LoadScreenCamera").GetComponent<Camera>();

        _canvasGroup.alpha = 1;
        _loadScreenCamera.enabled = true;
    }

    public static void FadeOut(float duration)
    {
        if (_canvasGroup == null) _canvasGroup = GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<CanvasGroup>();
        if(_loadScreenCamera == null) _loadScreenCamera = GameObject.FindGameObjectWithTag("LoadScreenCamera").GetComponent<Camera>();
        
        _canvasGroup.alpha = 0;
        _loadScreenCamera.enabled = false;
    }
}
