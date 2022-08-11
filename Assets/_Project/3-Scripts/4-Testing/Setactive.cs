using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setactive : MonoBehaviour
{
    public bool isTrue;
    public GameObject theObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTrue)
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }
        }
        if (isTrue)
        {
            theObject.SetActive(false);
        }
        else
        {
            theObject.SetActive(true);
        }
       
    }
}
