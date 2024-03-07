using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private GameObject _thirdPersonPlayer;
    private GameObject _player;
    private AudioSource _backgroundSourse;
    private AudioSource _playerWalkingSourse;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    // private void Start()
    // {
    //     _thirdPersonPlayer = GameObject.Find("ThirdPersonPlayer");
    //     _player = GameObject.Find("player_walking_stand");
    //     _backgroundSourse = _thirdPersonPlayer.GetComponent<AudioSource>();
    //     _playerWalkingSourse = _player.GetComponent<AudioSource>();
    // }
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        if (GameObject.Find("ThirdPersonPlayer") != null)
        {
            _thirdPersonPlayer = GameObject.Find("ThirdPersonPlayer");
            _backgroundSourse = _thirdPersonPlayer.GetComponent<AudioSource>();
            Debug.Log("ThirdPersonPlayer is got");
        }
        else 
            Debug.Log("ThirdPersonPlayer is null");
        if (GameObject.Find("player_walking_stand") != null)
        {
            _player = GameObject.Find("player_walking_stand");
            _playerWalkingSourse = _player.GetComponent<AudioSource>();
            Debug.Log("player is got");
        }
        else 
            Debug.Log("player is null");
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        Debug.Log(gameState);
        if (_playerWalkingSourse != null && _backgroundSourse != null)
        {
            if (gameState == GameState.Pause)
            {
                _backgroundSourse.volume = 0.1f;
                _playerWalkingSourse.volume = 0.0f;
            }
            else if (gameState == GameState.GamePlay)
            {
                _backgroundSourse.volume = 0.87f;
                _playerWalkingSourse.volume = 0.1f;
            }
        }
        else 
            Debug.Log("_playerWalkingSourse or _backgroundSourse is null !");
    }
}
