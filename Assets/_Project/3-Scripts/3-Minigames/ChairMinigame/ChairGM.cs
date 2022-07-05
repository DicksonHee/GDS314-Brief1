using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using TMPro;
using UnityEngine;

public class ChairGM : MinigameManager
{
    public int maxTime;
    public string nextScene;
    public TMP_Text text;
    
    private ChairAssignment chairAssignment;
    
    protected override void Awake()
    {
        base.Awake();

        chairAssignment = GetComponent<ChairAssignment>();
        Invoke(nameof(StartProtocol), _initialStartDelay);
        Invoke(nameof(StartTimer), _initialStartDelay);
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
        
        if (maxTime == 0) EndGameState();
    }

    protected override void EndGameState()
    {
        base.EndGameState();
        
        StopAllCoroutines();
        PlayerMovement.current.movementSpeed = 0;
        SceneLoad_Manager.LoadSpecificScene(nextScene);
    }

    public override void KillPlayer()
    {
        base.KillPlayer();
    }

    private void StartTimer()
    {
        StartCoroutine(DecrementGameTimer());
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

    private IEnumerator DecrementGameTimer()
    {
        maxTime--;
        text.text = "" + maxTime;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DecrementGameTimer());
    }
}
