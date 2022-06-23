using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using Scraper;
using UnityEngine;

public class LavaScraper : MinigameScraper
{  
    private void Awake()
    {
        _countList.Add("up", 0);
        _countList.Add("down", 0);
        _countList.Add("left", 0);
        _countList.Add("right", 0);
    }

    protected override void GetMessage(string author, string message)
    {
        if (_pollList.Count >= maxPollAmount)
        {
            RemoveMessage(_pollList.Dequeue());
        }
        
        _pollList.Enqueue(message);
        AddMessage(message);

        CalculateForce(out float xForce, out float zForce);
        PlayerMovement.OnApplyForce(xForce, zForce);
    }

    public override void GetMessageTest(string message)
    {
        if (_pollList.Count >= maxPollAmount)
        {
            RemoveMessage(_pollList.Dequeue());
        }
        
        _pollList.Enqueue(message);
        AddMessage(message);
        
        CalculateForce(out float xForce, out float zForce);
        PlayerMovement.OnApplyForce(xForce, zForce);
    }
    
    private void AddMessage(string message)
    {
        _countList[message]++;

        DebugMessage(_countList);
    }

    private void RemoveMessage(string message)
    {
        _countList[message]--;

        DebugMessage(_countList);
    }

    private void CalculateForce(out float xForce, out float zForce)
    {
        xForce = (float) _countList["right"] / _pollList.Count - (float) _countList["left"] / _pollList.Count;
        zForce = (float) _countList["up"] / _pollList.Count - (float) _countList["down"] / _pollList.Count;
        
        DebugMessage("" + xForce);
        DebugMessage("" + zForce);
    }
}
