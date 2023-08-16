using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStates _playerStates; //待確認：是否必須要public
    private CinemachineFreeLook _followCamera;
    private ControllerMovement3D _mainCamera;
    private CinemachineBrain _cinemachineBrain;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void RigisterPlayer(CharacterStates player)
    {
        _playerStates = player;

        /* get cinimachine new follow object after transition */
        _followCamera = FindObjectOfType<CinemachineFreeLook>();
        if (_followCamera != null)
        {
            _followCamera.Follow = _playerStates.transform;
            _followCamera.LookAt = _playerStates.transform;

        }

        /* set _mainCamera in ControllerMovement3D after transition */
        _mainCamera = FindObjectOfType<ControllerMovement3D>();
        if (_mainCamera != null)
        {
            _mainCamera._mainCamera = Camera.main.gameObject;
        }

        /* add CinemachineBrain component on main camera after transition */
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        if (_cinemachineBrain == null)
        {
            _cinemachineBrain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
            Debug.Log("CinemachineBrain component added to Main Camera.");
        }
        else 
        {
            Debug.Log("CinemachineBrain component already exists on Main Camera.");
        }
 
    }
}
