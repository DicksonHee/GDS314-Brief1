using System.Collections;
using System.Collections.Generic;
using PA.MinigameManager;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TimerAnimation : MonoBehaviour
{
    [Range(0f,1f)] public float maxAlphaVal;
    [Range(0,0.5f)]public float duration;

    public TMP_Text timerText;
    
    public void StartAnim(string currentTime)
    {
        timerText.text = currentTime; 
        transform.localRotation = Quaternion.Euler(0, 0, 50f);
        timerText.DOFade(maxAlphaVal, duration);
        transform.DOScale(Vector3.one, duration);
        transform.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.Flash);
        Invoke(nameof(EndAnim), duration);
    }

    public void EndAnim()
    {
        transform.DOScale(Vector3.zero, 0.9f - duration);
        timerText.DOFade(0, 0.9f - duration).SetEase(Ease.Flash);
    }
}
