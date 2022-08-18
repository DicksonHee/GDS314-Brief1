using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PA.MinigameManager;

public class MinigameUI : MonoBehaviour
{
    public MinigameManager minigameManager;

    // Start is called before the first frame update
    void Start()
    {
        minigameManager.OnGameEnded += GameEnded;
    }

    private void GameEnded()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
}
