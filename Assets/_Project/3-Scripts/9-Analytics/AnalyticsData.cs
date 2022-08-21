using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsData
{
    private static Dictionary<string, int> winLoseData;
    private static Dictionary<string, List<int>> timeRemainingData;
    private static Dictionary<string, List<Vector3>> losePositionData;

    public static void SaveWinLoseData(string key, int num)
    {
        if (!winLoseData.TryGetValue(key, out _)) winLoseData.Add(key, 0);
        
        winLoseData[key] += num;
        ES3.Save("WinLoseDict", winLoseData);
    }

    public static void SaveTimeRemainingData(string key, int num)
    {
        if (!timeRemainingData.TryGetValue(key, out _))
        {
            timeRemainingData.Add(key, new List<int>());
        }

        timeRemainingData[key].Add(num);
        ES3.Save("TimeRemainingDict", timeRemainingData);
    }

    public static void SaveLosePositionData(string key, Vector3 num)
    {
        if (!losePositionData.TryGetValue(key, out _))
        {
            losePositionData.Add(key, new List<Vector3>());
        }

        losePositionData[key].Add(num);
        ES3.Save("LosePositionDict", losePositionData);
    }

    public static void Load()
    {
        if (ES3.KeyExists("WinLoseDict")) winLoseData = (Dictionary<string, int>) ES3.Load("WinLoseDict");
        else winLoseData = new();

        if (ES3.KeyExists("TimeRemainingDict")) timeRemainingData = (Dictionary<string, List<int>>)ES3.Load("TimeRemainingDict");
        else timeRemainingData = new();

        if (ES3.KeyExists("LosePositionDict")) losePositionData = (Dictionary<string, List<Vector3>>)ES3.Load("LosePositionDict");
        else losePositionData = new();
    }
}
