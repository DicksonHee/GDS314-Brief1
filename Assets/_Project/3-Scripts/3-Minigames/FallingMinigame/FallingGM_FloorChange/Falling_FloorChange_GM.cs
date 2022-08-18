using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PA.MinigameManager
{
	public class Falling_FloorChange_GM : MinigameManager
	{
		private Falling_FloorChangeScraper floorChangeScraper;
		public Transform spawnPosition;

		public int initialGroup;
		public List<GameObject> floorGroupMeshes;
		public List<GameObject> floorGroupColliders;

		private GameObject currentActiveMeshGroup;
		private GameObject currentActiveColliderGroup;

		protected override void Awake()
		{
			floorChangeScraper = (Falling_FloorChangeScraper) scraper;
			InitialiseMeshAndColliders();

			PreGameState();
			Invoke(nameof(StartProtocol), _initialStartDelay);
			if(maxTime > 0) Invoke(nameof(StartTimer), _initialStartDelay + 2f);
		}

		private void OnEnable()
		{
			OnTimerStop += PlayerLose;
		}

		private void OnDisable()
		{
			OnTimerStop -= PlayerLose;
		}
		
		public void PlayerWin() => EndGame(true);
		public void PlayerLose() => EndGame();
		
		private void InitialiseMeshAndColliders()
		{
			for (int ii = 0; ii < floorGroupMeshes.Count; ii++)
			{
				if (ii == initialGroup)
				{
					floorGroupMeshes[ii].SetActive(true);
					floorGroupColliders[ii].SetActive(true);
				}
				else
				{
					floorGroupMeshes[ii].SetActive(false);
					floorGroupColliders[ii].SetActive(false);
				}
			}

			currentActiveMeshGroup = floorGroupMeshes[initialGroup];
			currentActiveColliderGroup = floorGroupColliders[initialGroup];
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void PreGameState()
		{
			base.PreGameState();
			PlayerMovement.current.SetCanMove(false);
			PlayerMovementActions.MovePlayerToLocation(spawnPosition.position, GameObject.FindGameObjectWithTag("Player"));
		}

		protected override void AcceptingInputsState()
		{
			base.AcceptingInputsState();
		}

		protected override void RunningInputsState()
		{
			base.RunningInputsState();
			ChangeFloorGroup(floorChangeScraper.GetFloorIndex());
		}

		protected override void NotAcceptingInputsState()
		{
			base.NotAcceptingInputsState();
			floorChangeScraper.ClearList();
		}

		protected override void EndGameState()
		{
			base.EndGameState();
		}

		public override void EndGame(bool hasWon = false)
		{
			StopAllCoroutines();
			PlayerMovement.current.movementSpeed = 0;

			base.EndGame(hasWon);
		}
		
		public override void KillPlayer()
		{
			PlayerMovement.current.SetTrigger("Fall");
			PlayerMovementActions.MovePlayerToLocation(spawnPosition.position, GameObject.FindGameObjectWithTag("Player"));
			PlayerMovement.current.SetCanMove(false);
		}

		protected override IEnumerator MinigameProtocol_CO()
		{
			AcceptingInputsState();
			yield return new WaitForSeconds(_acceptingInputDuration);
			RunningInputsState();
			NotAcceptingInputsState();
			yield return new WaitForSeconds(_notAcceptingInputDuration);

			StartCoroutine(MinigameProtocol_CO());
		}

		private void ChangeFloorGroup(int index)
		{
			for (int ii = 0; ii < floorGroupMeshes.Count; ii++)
			{
				if (ii == index)
				{
					StartCoroutine(ChangeFloorGroup_CO(ii));
				}
				else floorGroupMeshes[ii].SetActive(false);
			}
		}

		private IEnumerator ChangeFloorGroup_CO(int index)
		{
			float time = 0f;

			for (int ii = 0; ii < 5; ii++)
			{
				currentActiveMeshGroup.SetActive(false);
				floorGroupMeshes[index].SetActive(true);
				yield return new WaitForSeconds(time - 0.1f);
				currentActiveMeshGroup.SetActive(true);
				floorGroupMeshes[index].SetActive(false);
				yield return new WaitForSeconds(time);
				time += 0.15f;
			}

			currentActiveMeshGroup.SetActive(false);
			currentActiveColliderGroup.SetActive(false);

			floorGroupMeshes[index].SetActive(true);
			floorGroupColliders[index].SetActive(true);
			currentActiveMeshGroup = floorGroupMeshes[index];
			currentActiveColliderGroup = floorGroupColliders[index];
		}
	}
}