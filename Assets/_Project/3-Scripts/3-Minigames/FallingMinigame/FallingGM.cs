using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using UnityEngine;

namespace PA.MinigameManager
{
	public class FallingGM : MinigameManager
	{
		private LavaScraper lavaScraper;
		public Transform spawnPosition;

		protected override void Awake()
		{
			lavaScraper = (LavaScraper)scraper;
			PreGameState();
			Invoke(nameof(StartProtocol), _initialStartDelay);
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
			Debug.Log("Pre");
		}

		protected override void AcceptingInputsState()
		{
			base.AcceptingInputsState();
			Debug.Log("Accept");
		}

		protected override void RunningInputsState()
		{
			base.RunningInputsState();
			Debug.Log("Running");
			lavaScraper.ApplyChatInputForce();
		}

		protected override void NotAcceptingInputsState()
		{
			base.NotAcceptingInputsState();
			lavaScraper.ClearList();
			Debug.Log("NotAccept");
		}

		protected override void EndGameState()
		{
			base.EndGameState();
		}

		public override void EndGame()
		{
			StopAllCoroutines();
			PlayerMovement.current.movementSpeed = 0;
			SceneLoad_Manager.LoadSpecificScene(nextScene);
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
		
		
		public Vector2 GetForce() => lavaScraper.GetForce().normalized;
	}
}