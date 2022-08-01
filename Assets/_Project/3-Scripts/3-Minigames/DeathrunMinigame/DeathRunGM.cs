using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using UnityEngine;

public class DeathRunGM : MinigameManager
{
    public int maxTime;
    private DeathRunScrapper dRScraper;
    protected override void Awake()
    {
        base.Awake();
        dRScraper = (DeathRunScrapper)scraper;
        Invoke(nameof(StartProtocol), _initialStartDelay);

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void PreGameState()
    {
        base.PreGameState();
    }

    protected override void AcceptingInputsState()
    {
        base.AcceptingInputsState();

    }

    protected override void RunningInputsState()
    {
        base.RunningInputsState();

    }

    protected override void NotAcceptingInputsState()
    {
        base.NotAcceptingInputsState();

        if (maxTime == 0) EndGameState();
    }

    protected override void EndGameState()
    {
        base.EndGameState();

        StopAllCoroutines();
        PlayerMovement.current.movementSpeed = 0;
        SceneLoad_Manager.LoadSpecificScene(nextScene);
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

}
