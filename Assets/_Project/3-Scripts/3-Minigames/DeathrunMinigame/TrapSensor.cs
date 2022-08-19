using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSensor : MonoBehaviour
{
    public DeathRunGM deathRunGM;

    public Collider player;

    private void OnTriggerEnter(Collider other)
    {
        if (player == other)
        {
            Debug.Log("entered area");
            deathRunGM.inTriggerZone = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player == other)
        {
            Debug.Log("leaving area");
            deathRunGM.inTriggerZone = false;
        }
    }
}
