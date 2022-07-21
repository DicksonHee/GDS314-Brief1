using System;
using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using Scraper;
using UnityEngine;

public class LavaScraper : MinigameScraper
{
    private Vector2 currentForce;
    
    private void Awake()
    {
        _countList.Add("up", 0);
        _countList.Add("down", 0);
        _countList.Add("left", 0);
        _countList.Add("right", 0);
    }

    // specific controls and how the chat interacts with it per level with this script being one level
    public void ClearList()
    {
        for (int ii = 0; ii < _pollList.Count; ii++)
        {
            RemoveMessage(_pollList.Dequeue());
        }
    }

    public void ApplyChatInputForce()
    {
        CalculateForce(out float xForce, out float zForce);
        PlayerMovement.OnApplyForce(xForce, zForce);
    }

    private void CalculateForce(out float xForce, out float zForce)
    {
        xForce = 0;
        zForce = 0;
        if (_pollList.Count <= 0) return;
        xForce = (float) _countList["right"] / _pollList.Count - (float) _countList["left"] / _pollList.Count;
        zForce = (float) _countList["up"] / _pollList.Count - (float) _countList["down"] / _pollList.Count;
        currentForce = new Vector2(xForce, zForce);
    }
    
    public Vector2 GetForce() => currentForce;
}