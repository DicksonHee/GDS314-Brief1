using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public bool isFollowing;
    public float followSpeed;

    public Vector3 RotateAmount;

    public Transform followTarget; 

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);    
        }

        transform.Rotate(RotateAmount * Time.deltaTime);


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isFollowing)
            {
                isFollowing = true;
            }
        }
    }

}
