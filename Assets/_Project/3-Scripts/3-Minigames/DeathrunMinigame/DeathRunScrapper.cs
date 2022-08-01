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
    public float trapTimeOutTimer;
    private float trapTimerStart;

    public DeathrunPressureTrap currentTrap;

    private void Awake()
    {
        _countList.Add("now", 0);
        _countList.Add("hold", 0);

        voteHold = 0;
        voteNow = 0;
        trapTimerStart = trapTimeOutTimer;

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
        if (trapTimeOutTimer <= 0)
        {
            voteNow = _countList["now"];
            voteHold = _countList["hold"];

            temp = voteNow / (voteNow + voteHold);

            if (temp >= 0.7)
            {
                ClearList();
                currentTrap.ActivateTrap();

            }



        }
        

    }
    
}
