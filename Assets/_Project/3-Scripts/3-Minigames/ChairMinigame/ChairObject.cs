using PA.MinigameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChairObject : MonoBehaviour
{
	public float chairSpeed = 5f;

	private IEnumerator DestroyObject(float duration)
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	public void SpawnConfirmed(LaneDirections direction)
	{
		StopAllCoroutines();

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
	
		StartCoroutine(DestroyObject(4f));
	}

	public void SpawnCancelled()
	{
		Destroy(gameObject);
	}

	private IEnumerator MoveChair_CO(Vector3 moveDir)
	{
		yield return new WaitForSeconds(1);
		GetComponent<Rigidbody>().AddForce(moveDir * chairSpeed, ForceMode.Impulse);
	}

	private void MoveChair(Vector3 moveDir)
	{
		StartCoroutine(MoveChair_CO(moveDir));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MinigameManager.current.KillPlayer();
		}
	}
}