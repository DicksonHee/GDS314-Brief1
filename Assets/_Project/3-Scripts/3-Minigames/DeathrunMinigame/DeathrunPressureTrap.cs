using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathrunPressureTrap : MonoBehaviour
{

    public bool inTriggerZone;
    public GameObject[] triggerZone;
    public DeathRunGM deathRunGM;
    private int currentTrap;
    public GameObject activeTrigger;

    public void Awake()
    {
       currentTrap = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            inTriggerZone = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTriggerZone = false;
        }
    }

    public void NextTrap()
    {

        currentTrap++;
        activeTrigger = triggerZone[currentTrap];
        activeTrigger.SetActive(true);

    }


    public void ActivateTrap()
    {


        if (inTriggerZone)
        {
            deathRunGM.KillPlayer();
            // connect to death script/function and activate it
            
        }
        activeTrigger.SetActive(false);
        // play effects from each trap
        NextTrap();


    }

    

}
