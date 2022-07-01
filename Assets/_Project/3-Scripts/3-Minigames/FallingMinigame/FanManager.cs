using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using UnityEngine;

public class FanManager : MonoBehaviour
{
    private FanBehaviour[] _fanBehaviours;

    private void Awake()
    {
        _fanBehaviours = FindObjectsOfType<FanBehaviour>();
    }

    private void Update()
    {
        Vector3 forceOnPlayer = Vector3.zero;
        foreach (FanBehaviour fan in _fanBehaviours)
        {
            forceOnPlayer += fan.GetForceOnPlayer();
        }
        
        PlayerMovement.current.ApplyOutwardForce(forceOnPlayer);
    }
}
