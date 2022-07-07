using System;
using System.Collections;
using System.Collections.Generic;
using Scraper;
using UnityEngine;

public class MazeScraper : MinigameScraper
{
    private void Awake()
    {
        _countList.Add("cw", 0);
        _countList.Add("ccw", 0);
    }

    public int GetInput()
    {
        if (_pollList.Count <= 0) return 0;
        
        int retVal = _pollList.Peek() == "cw" ? 1 : -1;
        RemoveMessage(_pollList.Dequeue());
        return retVal;
    }
}
