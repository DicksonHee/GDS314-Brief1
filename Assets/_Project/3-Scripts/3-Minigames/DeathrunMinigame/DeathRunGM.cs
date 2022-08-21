using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using UnityEngine;

public class DeathRunGM : MinigameManager
{
    private DeathRunScrapper dRScraper;
    private int currentTrap;

    public Transform spawnPosition;
    public bool inTriggerZone;
    public DeathAnimation deathAnim;
    
    [Header("Traps")]
    public GameObject activeTrigger;
    public GameObject[] triggerZone;
    public GameObject[] trapEffects;

    protected override void Awake()
    {
        base.Awake();
        dRScraper = (DeathRunScrapper) scraper;
        currentTrap = 0;
        
        StartTimer();
        Invoke(nameof(StartProtocol), _initialStartDelay);
    }

    private void OnEnable()
    {
        OnTimerStop += PlayerLose;
    }

    private void OnDisable()
    {
        OnTimerStop -= PlayerLose;
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
    
    #region Trap Functions
    public void NextTrap()
    {
        // set the active trap that is currently set to be invisible
        // go to the next trap
        // turn on the next traps active
        activeTrigger.SetActive(false);
        currentTrap++;
        if (currentTrap < triggerZone.Length)
        {
            activeTrigger = triggerZone[currentTrap];
            activeTrigger.SetActive(true);
            Debug.Log("trap set");
        }
        
        // reset the timer and start it in the deathrun scrapper
        dRScraper.trapAfterActivation = dRScraper.trapTimerStart;
        dRScraper.startTimer = true;
    }

    public void ActivateTrap()
    {
        ActivateTrapEffect();

        Debug.Log("trap activated");
        if (inTriggerZone)
        {
            Debug.Log("killing player");
            deathAnim.UponDeath();
            Invoke(nameof(KillPlayer), 1f);
        }
        
        // play effects from each trap
        NextTrap();
    }

    private void ActivateTrapEffect()
    {
        if (trapEffects[currentTrap] != null)
        {
            trapEffects[currentTrap].SetActive(true);
        }
    }
    #endregion
    
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
