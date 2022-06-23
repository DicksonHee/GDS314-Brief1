using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SceneLoader
{
    public static void LoadSpecificScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public static void LoadPlayerScene() => SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
}
