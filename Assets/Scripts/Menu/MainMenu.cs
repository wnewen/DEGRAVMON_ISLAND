using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
    // private void Awake()
    // {
    //     EventHandler.CallAfterSceneLoadedEvent();
    // }
    public void PlayNewGame()
    {
        EventHandler.CallGameStateChangeEvent(GameState.NewGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public async void ResumeGame()
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        // SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1).completed += OnSceneLoaded;
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadScene(int sceneIndex)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        EventHandler.CallAfterSceneLoadedEvent();
    }
}
