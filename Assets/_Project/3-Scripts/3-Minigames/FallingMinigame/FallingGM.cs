using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

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
			
			lavaScraper.ApplyChatInputForce();
		}

		protected override void NotAcceptingInputsState()
		{
			base.NotAcceptingInputsState();
			
			lavaScraper.ClearList();
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
		
		public Vector2 GetForce() => lavaScraper.GetForce().normalized;
	}
}