using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChairObject : MonoBehaviour
{
	public float chairSpeed = 5f;

	private void Awake()
	{
		StartCoroutine(DestroyObject(4f));
	}

	private IEnumerator DestroyObject(float duration)
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	public void SpawnConfirmed(LaneDirections direction)
	{
		switch (direction)
		{
			case LaneDirections.N:
				MoveChair(new Vector3(0, 0, -1f));
				break;
			case LaneDirections.E:
				MoveChair(new Vector3(-1f, 0, 0));
				break;
			case LaneDirections.S:
				MoveChair(new Vector3(0, 0, 1f));
				break;
			case LaneDirections.W:
				MoveChair(new Vector3(1f, 0, 0));
				break;
		}

		StopAllCoroutines();
		StartCoroutine(DestroyObject(4f));
	}

	private void MoveChair(Vector3 moveDir)
	{
		GetComponent<Rigidbody>().AddForce(moveDir * chairSpeed, ForceMode.Impulse);
	}
}