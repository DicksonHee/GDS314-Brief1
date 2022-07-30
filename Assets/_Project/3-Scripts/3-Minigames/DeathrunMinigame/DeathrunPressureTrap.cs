using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathrunPressureTrap : MonoBehaviour
{

    public bool inTriggerZone;
    public GameObject[] triggerZone;
    public DeathRunGM deathRunGM;
    public GameObject[] traps;
    private int currentTrap;
    public GameObject activeTrap;

    public void Awake()
    {
       currentTrap = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            
            inTriggerZone = true;

        }
    }

    public void NextTrap()
    {

        currentTrap++;
        traps[currentTrap] = activeTrap;

    }


    public void ActivateTrap()
    {
        NextTrap();
        // play effects from each trap

        if (inTriggerZone)
        {
            deathRunGM.KillPlayer();
            // connect to death script/function and activate it
            
        }
       
    }

    

}
