using System;
using System.Collections;
using System.Collections.Generic;
using PA.MinigameManager;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public FallingGM fallingGm;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            fallingGm.KillPlayer();
        }
    }
}
