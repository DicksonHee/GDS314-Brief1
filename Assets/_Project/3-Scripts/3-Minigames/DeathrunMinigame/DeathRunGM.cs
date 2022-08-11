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
    
    public GameObject[] triggerZone;
    private int currentTrap;
    public GameObject activeTrigger;
    public DeathAnimation deathAnim;

    protected override void Awake()
    {
        base.Awake();
        dRScraper = (DeathRunScrapper) scraper;
        Invoke(nameof(StartProtocol), _initialStartDelay);
        currentTrap = 0;

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
        activeTrigger.SetActive(false);

        currentTrap++;
        activeTrigger = triggerZone[currentTrap];
        activeTrigger.SetActive(true);
        Debug.Log("trap set");
    }


    public void ActivateTrap()
    {

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

    protected override void NotAcceptingInputsState()
    {
        base.NotAcceptingInputsState();
    }

    protected override void EndGameState()
    {
        base.EndGameState();

        StopAllCoroutines();
        PlayerMovement.current.movementSpeed = 0;
        LoadElevatorScene();
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
