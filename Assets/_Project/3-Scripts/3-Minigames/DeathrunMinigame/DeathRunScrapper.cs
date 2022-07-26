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

    private void Awake()
    {
        _countList.Add("now", 0);
        _countList.Add("hold", 0);

        voteHold = 0;
        voteNow = 0;

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

        temp = voteNow / (voteHold + voteHold);

        if (temp <= 0.7)
        {
            ActivateTrap();
        }

    }
    
}
