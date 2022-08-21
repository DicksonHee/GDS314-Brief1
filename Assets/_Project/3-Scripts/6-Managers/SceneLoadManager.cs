using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoad_Manager
{
    public class StaticMB : MonoBehaviour
    {
    }

    private static StaticMB _staticMb;

    private static List<AsyncOperation> _scenesLoading = new();
    private static List<AsyncOperation> _scenesUnloading = new();

    private static void Init()
    {
        if (_staticMb != null) return;

        GameObject gameObject = new GameObject("StaticMB");
        _staticMb = gameObject.AddComponent<StaticMB>();
        GameObject.DontDestroyOnLoad(_staticMb);
    }

    public static void LoadSpecificScene(string sceneName)
    {
        Init();
        List<string> scenesToLoad = new List<string>();
        List<string> scenesToUnload = new List<string>();
        
        scenesToLoad.Add(sceneName);

        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name == "LoadScene") continue;
            else scenesToUnload.Add(scene.name);
        }
        
        _staticMb.StartCoroutine(ScenesLoading(scenesToLoad, scenesToUnload));
    }

    private static void LoadScene(string sceneName)
    {
        _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    private static void UnloadScene(string sceneName)
    {
        _scenesUnloading.Add(SceneManager.UnloadSceneAsync(sceneName));
    }

    private static IEnumerator ScenesLoading(List<string> scenesToLoad, List<string> scenesToUnload)
    {
        // Load Load Scene
        if (!SceneManager.GetSceneByName("LoadScene").isLoaded) LoadScene("LoadScene");
        while(!SceneManager.GetSceneByName("LoadScene").isLoaded)
        {
            yield return null;
        }
        if(_scenesLoading.Count > 0) _scenesLoading.RemoveAt(0);
        
        // Fade the screen to black
        LoadScreen.current.FadeIn(2f);
        yield return new WaitForSeconds(2f);
        
        foreach (string sceneName in scenesToUnload) UnloadScene(sceneName);
        bool allUnloaded = false;
        while (!allUnloaded)
        {
            allUnloaded = true;
            foreach (AsyncOperation op in _scenesUnloading)
            {
                if (!op.isDone) allUnloaded = false;
            }
            yield return null;
        }
        _scenesUnloading.Clear();
        
        foreach (string sceneName in scenesToLoad) LoadScene(sceneName);
        bool allLoaded = false;
        while (!allLoaded)
        {
            allLoaded = true;
            foreach (AsyncOperation op in _scenesLoading)
            {
                if (!op.isDone) allLoaded = false;
            }
            yield return null;
        }
        _scenesLoading.Clear();
        
        // Fade the screen to clear
        LoadScreen.current.FadeOut(2f);
    }
}
