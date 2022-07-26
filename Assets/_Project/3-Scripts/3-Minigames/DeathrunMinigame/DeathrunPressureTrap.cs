using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathrunPressureTrap : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            // get player script and activate kill function
        }
    }

    public void ActivateTrap()
    {

       
    }

    

}
