using System.Collections;
using System.Collections.Generic;
using PA.MinigameManager;
using UnityEngine;

public class ChairGM : MinigameManager
{
    protected override void Awake()
    {
        base.Awake();
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
    }

    protected override void EndGameState()
    {
        base.EndGameState();
    }

    public override void KillPlayer()
    {
        base.KillPlayer();
    }

    protected override IEnumerator MinigameProtocol_CO()
    {
        return base.MinigameProtocol_CO();
    }
}
