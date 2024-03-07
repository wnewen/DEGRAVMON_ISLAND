using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool _pauseMenuOpen;
    [SerializeField] private GameObject _pauseMenu;
    
    void Update()
    {
        TogglePauseMenu();
    }

    private void TogglePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuOpen = !_pauseMenuOpen;
            _pauseMenu.SetActive(_pauseMenuOpen);
            if (_pauseMenuOpen)
                EventHandler.CallGameStateChangeEvent(GameState.Pause);               
            else
                EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }

    public void BackToMainMenu()
    {
        EventHandler.CallBeforeSceneUnloadEvent();
        SceneManager.LoadSceneAsync("MainMenu");
        EventHandler.CallAfterSceneLoadedEvent();
    }
}
