using System;
using System.Collections;
using System.Collections.Generic;
using PA.MinigameManager;
using UnityEngine;

public class ForceArrow : MonoBehaviour
{
    public FallingGM GM;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float degrees = Mathf.Atan2(GM.GetForce().y, GM.GetForce().x) * Mathf.Rad2Deg;
//        Debug.Log(degrees);
        rectTransform.rotation = Quaternion.Euler(0, 0, degrees);
    }
}