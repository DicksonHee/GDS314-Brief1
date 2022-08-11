using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using Scraper;
using UnityEngine;

public class Falling_FloorChangeScraper : MinigameScraper
{
    private void Awake()
    {
        _countList.Add("1", 0);
        _countList.Add("2", 0);
        _countList.Add("3", 0);
    }

    // specific controls and how the chat interacts with it per level with this script being one level
    public void ClearList()
    {
        for (int ii = 0; ii < _pollList.Count; ii++)
        {
            RemoveMessage(_pollList.Dequeue());
        }
    }

    public int GetFloorIndex()
    {
        int highestVote = _countList["1"];
        int floorIndex = 0;

        if (_countList["2"] > highestVote)
        {
            highestVote = _countList["2"];
            floorIndex = 1;
        }

        if (_countList["3"] > highestVote)
        {
            highestVote = _countList["3"];
            floorIndex = 2;
        }

        return floorIndex;
    }
}