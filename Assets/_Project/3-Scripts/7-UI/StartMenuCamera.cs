using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCamera : MonoBehaviour
{
    private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha8)) _animator.SetTrigger("Left");
		if (Input.GetKeyDown(KeyCode.Alpha9)) _animator.SetTrigger("Mid");
		if (Input.GetKeyDown(KeyCode.Alpha0)) _animator.SetTrigger("Right");
    }
}
