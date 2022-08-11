using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextTrap : MonoBehaviour
{
    public DeathRunGM dRGM;
    [SerializeField]
    Collider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (playerCollider == other)
        {
            dRGM.NextTrap();
        }
    }

}
