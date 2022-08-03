using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SessionManager : MonoBehaviour
{
    public static SessionManager current;
    public List<MinigameData> minigameData;
    public List<MinigameType> minigameTypes;
    
    private List<MinigameType> playedMinigames = new();
    private List<string> playedScenes = new();
    
    private void Awake()
    {
        current = this;
        
        ShuffleMinigameDataList();
        DontDestroyOnLoad(gameObject);
    }

    public string GetNextRandomScene()
    {
        MinigameType typeToLoad = MinigameType.None;
        string sceneToLoad = "";
        
        ShufflePlayedMinigamesList();
        foreach (MinigameType minigameType in minigameTypes)
        {
            if (!playedMinigames.Contains(minigameType))
            {
                typeToLoad = minigameType;
                break;
            }
        }

        MinigameData chosenData = null;
        foreach (MinigameData data in minigameData)
        {
            if (data.minigameType == typeToLoad)
            {
                sceneToLoad = data.minigameSceneName;
                chosenData = data;
                break;
            }
        }

        if(chosenData != null) minigameData.Remove(chosenData);
        return sceneToLoad;
    }
    
    private void ShufflePlayedMinigamesList()
    {
        for (int ii = 0; ii < playedMinigames.Count; ii++)
        {
            MinigameType temp = playedMinigames[ii];
            int randomiser = Random.Range(ii, playedMinigames.Count);
            playedMinigames[ii] = playedMinigames[randomiser];
            playedMinigames[randomiser] = temp;
        }
    }
    
    private void ShuffleMinigameDataList()
    {
        for (int ii = 0; ii < minigameData.Count; ii++)
        {
            MinigameData temp = minigameData[ii];
            int randomiser = Random.Range(ii, minigameData.Count);
            minigameData[ii] = minigameData[randomiser];
            minigameData[randomiser] = temp;
        }
    }
}

[Serializable]
public class MinigameData
{
    public MinigameType minigameType;
    public string minigameSceneName;
}

public enum MinigameType
{
    Lava,
    Gridiron,
    Chase,
    None
}
