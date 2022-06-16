using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 cameraOffset;
    public float horizontalDeadzone;
    public float verticalDeadzone;

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        //transform.position = cameraTarget.position + cameraOffset;
        transform.DOMove(cameraTarget.position + cameraOffset, 0.075f).SetEase(Ease.InOutQuad);
    }
}
