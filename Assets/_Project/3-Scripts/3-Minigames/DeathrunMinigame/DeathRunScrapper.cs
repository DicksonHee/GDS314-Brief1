using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;

public class DeathRunScrapper : MinigameScraper
{

    private bool majorityReached;
    private float voteHold;
    private float voteNow;
    private float temp;
    public float trapAfterActivation;
    private float trapTimerStart;
    public bool startTimer;

    public DeathRunGM deathRunGameManager;
    

    private void Awake()
    {
        _countList.Add("now", 0);
        _countList.Add("hold", 0);

        voteHold = 0;
        voteNow = 0;
        trapTimerStart = trapAfterActivation;
        startTimer = true;

    }

    private void FixedUpdate()
    {
        MajorityVote();
    }


    public void ClearList()
    {
        for (int ii = 0; ii < _pollList.Count; ii++)
        {
            RemoveMessage(_pollList.Dequeue());
        }
    }

    public void MajorityVote()
    {
        voteNow = _countList["now"];
        voteHold = _countList["hold"];



        if (trapAfterActivation >= 0 && startTimer)
        {
            
            trapAfterActivation -= Time.deltaTime;
        }

        if (trapAfterActivation <= 0)
        {
            
            Debug.Log(temp);



            startTimer = false;

            temp = voteNow / (voteNow + voteHold);

            if (temp >= 0.7 || deathRunGameManager.NextTrap)
            {
                ClearList();
                deathRunGameManager.ActivateTrap();
                trapAfterActivation = trapTimerStart;
                startTimer = true;

            }



        }
        

    }
    
}
