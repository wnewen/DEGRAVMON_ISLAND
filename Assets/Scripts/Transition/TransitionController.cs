using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class TransitionController : Singleton<TransitionController>
{
    [SerializeField] private GameObject _playerPrefab;
    private GameObject _player;
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private bool _isRoomSceneTransition;
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    [SerializeField] private float _fadeDuration;
    private bool _isFade;
    private bool _canTransition = true;

    
    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        // _canTransition = gameState == GameState.GamePlay;
        // Debug.Log("_canTransition = " + _canTransition + " now !");
    }
    
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        if (!_isFade && _canTransition)
        {
            switch (transitionPoint._transitonType)
            {
                case TransitionPoint.TransitionType.SameScene:
                    StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint._destinationTag));
                    break;
                case TransitionPoint.TransitionType.RoomToScene:
                    StartCoroutine(Transition(transitionPoint._sceneName, transitionPoint._destinationTag));
                    break;
                case TransitionPoint.TransitionType.RoomToRoom:
                    _isRoomSceneTransition = true;
                    StartCoroutine(Transition(transitionPoint._sceneName, transitionPoint._destinationTag));
                    break;
                case TransitionPoint.TransitionType.SceneToRoom:
                    _isRoomSceneTransition = true;
                    StartCoroutine(Transition(transitionPoint._sceneName, transitionPoint._destinationTag));
                    break;
                case TransitionPoint.TransitionType.DifferentScene:
                    StartCoroutine(Transition(transitionPoint._sceneName, transitionPoint._destinationTag));
                    break;
            }
        }
        
    }
    


    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        yield return Fade(1);
        EventHandler.CallBeforeSceneUnloadEvent();
        if ((SceneManager.GetActiveScene().name != sceneName) && _isRoomSceneTransition)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            _isRoomSceneTransition = false;
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
            yield break;
        }
        else if (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject existPlayer = GameObject.FindGameObjectWithTag("Player");
                Destroy(existPlayer);
                yield return Instantiate(_playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            }
            else
            {
                yield return Instantiate(_playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            }
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
            yield break;
        }
        /* transition in the same scene */
        else
        {
            /* get components on player */
            _player = GameManager.Instance._playerStates.gameObject;
            _playerInput = _player.GetComponent<PlayerInput>();
            _characterController = _player.GetComponent<CharacterController>();

            /* turn off components to transition */
            _playerInput.enabled = false;
            _characterController.enabled = false;
            Debug.Log("player is at " + _player.transform.position);
            _player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            /* after transition turn on components */
            _playerInput.enabled = true;
            _characterController.enabled = true;
            Debug.Log("transition to : " + GetDestination(destinationTag).transform.position);
            Debug.Log("player is at " + _player.transform.position + "after transition ");
            yield return null;
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        _isFade = true;
        _fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(_fadeCanvasGroup.alpha - targetAlpha) / _fadeDuration;
        while (!Mathf.Approximately(_fadeCanvasGroup.alpha, targetAlpha))
        {
            _fadeCanvasGroup.alpha = Mathf.MoveTowards(_fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        _fadeCanvasGroup.blocksRaycasts = false;
        _isFade = false;

    }
    

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();

        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i]._destinationTag == destinationTag)
                return entrances[i];
        }
        return null;
    }
}


