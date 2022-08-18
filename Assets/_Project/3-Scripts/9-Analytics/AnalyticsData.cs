using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsData
{
    private static Dictionary<string, int> winLoseData;
    private static Dictionary<string, List<int>> timeRemainingData;
    private static Dictionary<string, List<Vector3>> losePositionData;
    private static readonly string saveLocation = "C:/Users/dhee/Desktop/saveFile.txt";

    public static void SaveWinLoseData(string key, int num)
    {
        if (!winLoseData.TryGetValue(key, out _)) winLoseData.Add(key, 0);
        
        winLoseData[key] += num;
        ES3.Save("WinLoseDict", winLoseData, saveLocation);
    }

    public static void SaveTimeRemainingData(string key, int num)
    {
        if (!timeRemainingData.TryGetValue(key, out _))
        {
            timeRemainingData.Add(key, new List<int>());
        }

        timeRemainingData[key].Add(num);
        ES3.Save("TimeRemainingDict", timeRemainingData, saveLocation);
    }

    public static void SaveLosePositionData(string key, Vector3 num)
    {
        if (!losePositionData.TryGetValue(key, out _))
        {
            losePositionData.Add(key, new List<Vector3>());
        }

        losePositionData[key].Add(num);
        ES3.Save("LosePositionDict", losePositionData, saveLocation);
    }

    public static void Load()
    {
        if (ES3.KeyExists("WinLoseDict", saveLocation)) winLoseData = (Dictionary<string, int>) ES3.Load("WinLoseDict", saveLocation);
        else winLoseData = new();

        if (ES3.KeyExists("TimeRemainingDict", saveLocation)) timeRemainingData = (Dictionary<string, List<int>>)ES3.Load("TimeRemainingDict", saveLocation);
        else timeRemainingData = new();

        if (ES3.KeyExists("LosePositionDict", saveLocation)) losePositionData = (Dictionary<string, List<Vector3>>)ES3.Load("LosePositionDict", saveLocation);
        else losePositionData = new();
    }
}
