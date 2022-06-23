using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObject : MonoBehaviour
{
	public float chairSpeed = 5f;
	private LaneDirections direction = LaneDirections.None;


	private void Awake()
	{
		StartCoroutine(DestroyObject(4f));
	}

	private void Update()
	{
		switch (direction)
		{
			case LaneDirections.N:
				transform.position += new Vector3(0, 0, -1f) * Time.deltaTime * chairSpeed;
				break;
			case LaneDirections.E:
				transform.position += new Vector3(-1f, 0, 0) * Time.deltaTime * chairSpeed;
				break;
			case LaneDirections.S:
				transform.position += new Vector3(0, 0, 1f) * Time.deltaTime * chairSpeed;
				break;
			case LaneDirections.W:
				transform.position += new Vector3(1f, 0, 0) * Time.deltaTime * chairSpeed;
				break;
		}
	}

	private IEnumerator DestroyObject(float duration)
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	public void SpawnConfirmed(LaneDirections direction)
	{
		this.direction = direction;
		StopAllCoroutines();
		StartCoroutine(DestroyObject(4f));
	}
}
