using System;
using System.Collections;
using System.Collections.Generic;
using PA.MinigameManager;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public FallingGM FallingGm;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FallingGm.EndGame();
        }
    }
}
