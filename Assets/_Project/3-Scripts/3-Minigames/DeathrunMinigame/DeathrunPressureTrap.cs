using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathrunPressureTrap : MonoBehaviour
{

    public bool inTriggerZone;
    public GameObject triggerZone;
    public DeathRunGM deathRunGM;

    public void Awake()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            
            inTriggerZone = true;

        }
    }

    public void ActivateTrap()
    {

        // play effects from each trap

        if (inTriggerZone)
        {
            deathRunGM.KillPlayer();
            // connect to death script/function and activate it
        }
       
    }

    

}
