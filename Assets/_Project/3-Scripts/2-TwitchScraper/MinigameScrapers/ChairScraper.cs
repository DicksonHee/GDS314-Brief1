using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;

public class ChairScraper : MinigameScraper
{
    private void Awake()
    {
        _countList.Add("up", 0);
        _countList.Add("down", 0);
        _countList.Add("left", 0);
        _countList.Add("right", 0);
    }

    public List<LaneDirections> CalculateHighest(int laneAmount)
    {
        var _sortedDict = from entry in _countList orderby entry.Value descending select entry;
        List<LaneDirections> sortedDirections = new();

        foreach (var entry in _sortedDict)
        {
            if (laneAmount > 0)
            {
                laneAmount--;
                switch (entry.Key)
                {
                    case "up":
                        sortedDirections.Add(LaneDirections.N);
                        break;
                    case "down":
                        sortedDirections.Add(LaneDirections.S);
                        break;
                    case "left":
                        sortedDirections.Add(LaneDirections.W);
                        break;
                    case "right":
                        sortedDirections.Add(LaneDirections.E);
                        break;
                }
            }
            else
            {
                break;
            }
        }

        string debugMessage = "";
        foreach (var item in _countList)
        {
            debugMessage += item.Key + ": " + item.Value;
        }

        return sortedDirections;
    }
}
