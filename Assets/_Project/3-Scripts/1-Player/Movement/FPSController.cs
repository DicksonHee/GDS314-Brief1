using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float xRotation;
    private float yRotation;

    public Transform cam;

    public float sensX;
    public float sensY;
    public float multiplier;
    public Vector2 xRotLimits;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Find current look rotation
        Vector3 rot = cam.transform.localRotation.eulerAngles;
        yRotation = rot.y + mouseX * sensX * Time.fixedDeltaTime * multiplier;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY * sensY * Time.fixedDeltaTime * multiplier;
        xRotation = Mathf.Clamp(xRotation, xRotLimits.x, xRotLimits.y);

        //Perform the rotations
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
