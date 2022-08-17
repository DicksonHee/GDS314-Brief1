using PA.MinigameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChairObject : MonoBehaviour
{
	public float chairSpeed = 5f;
	public float rotationOfChair;

	private IEnumerator DestroyObject(float duration)
	{
		// wait for the duration stated in the inspector and then destroy the chair object
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	public void SpawnConfirmed(LaneDirections direction)
	{
		StopAllCoroutines();

		switch (direction)
		{
			case LaneDirections.N:
				MoveChair(new Vector3(0, 0, -1f), Quaternion.Euler(0,rotationOfChair + 180, 0));
				break;
			case LaneDirections.E:
				MoveChair(new Vector3(-1f, 0, 0), Quaternion.Euler(0, rotationOfChair - 90, 0));
				break;
			case LaneDirections.S:
				MoveChair(new Vector3(0, 0, 1f), Quaternion.Euler(0, rotationOfChair, 0));
				break;
			case LaneDirections.W:
				MoveChair(new Vector3(1f, 0, 0), Quaternion.Euler(0, rotationOfChair + 90, 0));
				break;
		}
	
		StartCoroutine(DestroyObject(4f));
	}

	public void SpawnCancelled()
	{
		// when the spawn is cancelled for whatever reason destroy all of the gameobjects that are part of it
		Destroy(gameObject);
	}

	private void MoveChair(Vector3 moveDir, Quaternion rotation)
	{
		//yield return new WaitForSeconds(0.5f);
		GetComponent<Transform>().rotation = rotation;
		GetComponent<Rigidbody>().AddForce(moveDir * chairSpeed, ForceMode.Impulse);
	}

	// private void MoveChair(Vector3 moveDir)
	// {
	// 	//StartCoroutine(MoveChair_CO(moveDir));
	// }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MinigameManager.current.EndGame();
		}
	}
}