using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSensor : MonoBehaviour
{
    private DeathrunPressureTrap pressureTrap;

    private void Awake()
    {
        pressureTrap = GetComponentInParent<DeathrunPressureTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("entered area");
            pressureTrap.inTriggerZone = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("leaving area");
            pressureTrap.inTriggerZone = false;
        }
    }
}
