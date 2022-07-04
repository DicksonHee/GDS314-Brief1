using System;
using System.Collections;
using System.Collections.Generic;
using Scraper;
using Testing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu_UI : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionsPanel;

    public TMP_InputField inputField;
    public string firstSceneName;
    
    private void Start()
    {
        startPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OpenStartPanel() => startPanel.SetActive(true);
    public void CloseStartPanel() => startPanel.SetActive(false);
    public void OpenOptionsPanel() => optionsPanel.SetActive(true);
    public void CloseOptionsPanel() => optionsPanel.SetActive(false);
    public void QuitGame() => Application.Quit();

    public void StartGame()
    {
        SessionData.twitchChannelName = inputField.text.ToLower();
        GameObject chatReader = new GameObject();
        chatReader.AddComponent<ChatReader>();
        chatReader.name = "ChatReader";
        if(SessionData.twitchChannelName == "test") chatReader.AddComponent<RandomChatInputs>();
        SceneLoad_Manager.LoadSpecificScene(firstSceneName);
    }
}
