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
    public StaticTVShader _staticShader;
    public Animator _cameraAnimator;
    public Animator _canvasAnimator;

    public TMP_InputField inputField;
    public string firstSceneName;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenStartPanel()
    {
        _canvasAnimator.SetTrigger("FadeMain");
    }
    public void CloseStartPanel()
    {

    }
    public void OpenOptionsPanel()
    {

    }
    public void CloseOptionsPanel()
    {

    }
    public void OpenCreditsPanel()
    {

    }

    public void CloseCreditsPanel()
    {

    }

    public void QuitGame()
    {

    }

    public void EnterPressed()
    {
        StartCoroutine(OpenStartPanel_CO());
    }
    
    private IEnumerator OpenStartPanel_CO()
    {
        _canvasAnimator.SetTrigger("InputScreenFade");
        _staticShader.StaticOn(StaticScreenPos.Mid);
        yield return new WaitForSeconds(2f);
        _staticShader.StaticOff(StaticScreenPos.Mid);
        yield return new WaitForSeconds(1f);
        StartGame();
    }

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
