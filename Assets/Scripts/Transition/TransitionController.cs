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

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint._transitonType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint._destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitionPoint._sceneName, transitionPoint._destinationTag));
                break;
        }
    }
    


    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        //TODO:保存數據
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Instantiate(_playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            yield break;
        }
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
        }
        
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


