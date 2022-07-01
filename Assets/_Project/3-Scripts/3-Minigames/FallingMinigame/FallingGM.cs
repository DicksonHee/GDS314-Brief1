using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PA.MinigameManager
{
	public class FallingGM : MinigameManager
	{
		LavaScraper lavaScraper;

		protected override void Awake()
		{
			lavaScraper = (LavaScraper)scraper;
			StartCoroutine(MinigameProtocol_CO());
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void PreGameState()
		{
			base.PreGameState();
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
			Debug.Log("NotAccept");
		}

		protected override void EndGameState()
		{
			base.EndGameState();
		}

		protected override IEnumerator MinigameProtocol_CO()
		{
			PreGameState();
			yield return new WaitForSeconds(_initialStartDelay);
			AcceptingInputsState();
			yield return new WaitForSeconds(_acceptingInputDuration);
			RunningInputsState();
			NotAcceptingInputsState();
			yield return new WaitForSeconds(_notAcceptingInputDuration);

			StartCoroutine(MinigameProtocol_CO());
		}
	}
}