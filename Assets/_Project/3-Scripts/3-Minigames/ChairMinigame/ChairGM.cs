using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChairGM : MinigameManager
{
    private ChairAssignment chairAssignment;
    
    protected override void Awake()
    {
        base.Awake();

        chairAssignment = GetComponent<ChairAssignment>();
        Invoke(nameof(StartProtocol), _initialStartDelay);
        if(maxTime > 0) Invoke(nameof(StartTimer), _initialStartDelay);
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
        
        chairAssignment.ShowLanes();
    }

    protected override void RunningInputsState()
    {
        base.RunningInputsState();
        
        chairAssignment.ConfirmLanes();
    }

    protected override void NotAcceptingInputsState()
    {
        base.NotAcceptingInputsState();
        
        if (maxTime <= 0) PlayerWin();
    }

    public override void EndGame(bool hasWon = false)
    {
        EndGameState();
        StopAllCoroutines();
        PlayerMovement.current.movementSpeed = 0;
        
        if (hasWon)
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Level Win" + SceneManager.GetActiveScene(), 
                new Dictionary<string, object> { { "Time Remaining", maxTime } });
            LoadElevatorScene();
        }
        else
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Level Lose" + SceneManager.GetActiveScene(), 
                new Dictionary<string, object> { { "Position On Lose", GameObject.FindGameObjectWithTag("Player").transform.position } });
            LoadMainMenuScene();
        }
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
