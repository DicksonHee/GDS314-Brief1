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
    public DeathAnimation deathAnim;

    public void Awake()
    {
       currentTrap = 0;
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ActivateTrap();
        }
    }


    public void NextTrap()
    {

        currentTrap++;
        activeTrigger = triggerZone[currentTrap];
        activeTrigger.SetActive(true);
        Debug.Log("trap set");
    }


    public void ActivateTrap()
    {

        Debug.Log("trap activated");
        if (inTriggerZone)
        {
            Debug.Log("killing player");
            deathAnim.UponDeath();
            // connect to death script/function and activate it

        }
        activeTrigger.SetActive(false);
        // play effects from each trap
        NextTrap();


    }

    

}
