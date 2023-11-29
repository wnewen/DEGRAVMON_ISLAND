using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool _pauseMenuOpen;
    [SerializeField] private GameObject _pauseMenu;
    
    void Update()
    {
        OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuOpen = !_pauseMenuOpen;
            _pauseMenu.SetActive(_pauseMenuOpen);
        }
    }
}
