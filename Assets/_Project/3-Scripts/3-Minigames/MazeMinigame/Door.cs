using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Opening");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
         
            transform.Rotate(0, 180, 0);
            Debug.Log("Opening");
                    
        }
    }
}
