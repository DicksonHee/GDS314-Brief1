using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using UnityEngine;

public class DeathRunGM : MinigameManager
{
    public int maxTime;
    private DeathRunScrapper dRScraper;

    public bool inTriggerZone;
    public bool resetTimer;
    
    public GameObject[] triggerZone;
    private int currentTrap;
    public GameObject activeTrigger;
    public DeathAnimation deathAnim;
    public GameObject[] trapEffects;

    public float timeTillEmitterEnd;
    private float emitterStartTime;
    public bool emitterTimerStart;
    private GameObject emmiterToBeDeactivated;

    protected override void Awake()
    {
        base.Awake();
        dRScraper = (DeathRunScrapper) scraper;
        Invoke(nameof(StartProtocol), _initialStartDelay);
        currentTrap = 0;
        emitterTimerStart = false;

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

    public void NextTrap()
    {
        // set the active trap that is currently set to be invisible
        // go to the next trap
        // turn on the next traps active
        activeTrigger.SetActive(false);
        currentTrap++;
        activeTrigger = triggerZone[currentTrap];
        activeTrigger.SetActive(true);
        Debug.Log("trap set");



        // reset the timer and start it in the deathrun scrapper
        dRScraper.trapAfterActivation = dRScraper.trapTimerStart;
        dRScraper.startTimer = true;


    }


    public void ActivateTrapEffect()
    {
        if (trapEffects[currentTrap] != null)
        {
            trapEffects[currentTrap].SetActive(true);
        }
        
    }

    public void ActivateTrap()
    {
        ActivateTrapEffect();

        Debug.Log("trap activated");
        if (inTriggerZone)
        {
            Debug.Log("killing player");
            deathAnim.UponDeath();
            
            
            // connect to death script/function and activate it

        }
        
        // play effects from each trap
        NextTrap();

    }

    public override void EndGame(bool hasWon = false)
    {
        StopAllCoroutines();
        PlayerMovement.current.movementSpeed = 0;
        LoadElevatorScene();
    }

    protected override void NotAcceptingInputsState()
    {
        base.NotAcceptingInputsState();
    }

    protected override void EndGameState()
    {
        base.EndGameState();
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
