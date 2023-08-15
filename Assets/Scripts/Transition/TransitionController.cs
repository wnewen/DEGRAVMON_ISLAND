using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.AI;
using UnityEngine.InputSystem;


public class TransitionController : Singleton<TransitionController>
{
    private GameObject _player;
    // private NavMeshAgent _playerAgent;
    private PlayerInput _playerInput;
    private CharacterController _characterController;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("TransitionController Awake");
    }



    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint._transitonType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint._destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                break;
        }
    }
    


    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        _player = GameManager.Instance._playerStates.gameObject;
        // _playerAgent = _player.GetComponent<NavMeshAgent>();
        _playerInput = _player.GetComponent<PlayerInput>();
        _characterController = _player.GetComponent<CharacterController>();
        // _playerAgent.enabled = false;
        _playerInput.enabled = false;
        _characterController.enabled = false;
        Debug.Log("player is at " + _player.transform.position);
        _player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        // _playerAgent.enabled = true;
        _playerInput.enabled = true;
        _characterController.enabled = true;
        Debug.Log("transition to : " + GetDestination(destinationTag).transform.position);
        Debug.Log("player is at " + _player.transform.position);
        yield return null;
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


