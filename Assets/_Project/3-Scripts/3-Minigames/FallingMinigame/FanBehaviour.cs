using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FanBehaviour : MonoBehaviour
{
    public float fanStrength;
    public float fanArc;
    public float fanSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, fanArc, 0), fanSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
