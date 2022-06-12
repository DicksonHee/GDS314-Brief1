using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using Scraper;
using UnityEngine;

public class LavaScraper : MinigameScraper
{
    public int maxPollAmount = 100;
    public bool showDebug;
    
    private readonly Queue<string> _pollList = new();
    private readonly List<int> _countList = new();
    
    private void Awake()
    {
        for (int ii = 0; ii < 4; ii++)
        {
            _countList.Add(0);
        }
    }

    protected override void GetMessage(string author, string message)
    {
        if (_pollList.Count < maxPollAmount)
        {
            _pollList.Enqueue(message);
            AddMessage(message);
        }
        else
        {
            RemoveMessage(_pollList.Dequeue());
        }
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
        if (message.Contains("up")) _countList[0]++;
        else if (message.Contains("down")) _countList[1]++;
        else if (message.Contains("left")) _countList[2]++;
        else if (message.Contains("right")) _countList[3]++;
        
        DebugMessage("Up: " + _countList[0] + " Down: " + _countList[1] + " Left: " + _countList[2] + " Right: " + _countList[3]);
    }

    private void RemoveMessage(string message)
    {
        if (message.Contains("up")) _countList[0]--;
        else if (message.Contains("down")) _countList[1]--;
        else if (message.Contains("left")) _countList[2]--;
        else if (message.Contains("right")) _countList[3]--;
        
        DebugMessage("Up: " + _countList[0] + " Down: " + _countList[1] + " Left: " + _countList[2] + " Right: " + _countList[3]);
    }

    private void CalculateForce(out float xForce, out float zForce)
    {
        xForce = (float) _countList[3] / _pollList.Count - (float) _countList[2] / _pollList.Count;
        zForce = (float) _countList[0] / _pollList.Count - (float) _countList[1] / _pollList.Count;
        
        DebugMessage("" + xForce);
        DebugMessage("" + zForce);
    }
    
    private void DebugMessage(string debugMessage)
    {
        if (!showDebug) return;
        
        Debug.Log(debugMessage);
    }
}
