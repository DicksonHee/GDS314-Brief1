using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float distanceTillDest;
    public AnimationCurve animCurve;
    public Transform otherPosition;
    private Vector3 nextPosition;

    public void OpenDoor()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            nextPosition = Vector3.Lerp(transform.position, otherPosition.position, animCurve.Evaluate(distanceTillDest * Time.deltaTime));
            transform.position = nextPosition;
        }

    }
    private void Update()
    {
        OpenDoor();
    }

}
