using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenUI : MonoBehaviour
{
    public Animator animator;

    private float startTime;
    private bool hasEnded;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !hasEnded && Time.time - startTime > 2f)
        {
            hasEnded = true;
            animator.SetTrigger("End");
            Invoke(nameof(LoadMainMenu), 2f);
        }
    }

    private void LoadMainMenu()
    {
        SceneLoad_Manager.LoadSpecificScene("NewStartScene");
    }
}
