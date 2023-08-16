using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{

    private GameObject _player;

    [Header("Detect Info")]
    [SerializeField] private  GameObject _transitionButton;
    [SerializeField] private float _detectionRadius = 5.0f;
    private bool _playerWasInside = false; // check player is inside the detection range 
        
    [Header("Transition Info")]
    public string _sceneName; // 需要被TransitionController存取
    /* define transition type choice */
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    public TransitionType _transitonType; // 需要被TransitionController存取
    public TransitionDestination.DestinationTag _destinationTag; // 需要被TransitionController存取

    private void Update() 
    {
        Vector3 _switcherPositon = transform.position;

        /* collect colliders in radius */
        Collider[] _colliders = Physics.OverlapSphere(_switcherPositon, _detectionRadius);

        bool playerInside = false; /* check whether player in range */

        /* check there is player in colliders */
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerInside = true;
                break;
            }
            else
            {
                playerInside = false;
            }
        }

        /* player enter the detection range */
        if (playerInside)
        {
            // Debug.Log("Player entered the detection zone!");
            _transitionButton.SetActive(true);
        }
        /* player exit the detection range */
        else if (!playerInside && _playerWasInside)
        {
            // Debug.Log("Player exited the detection zone!");
            _transitionButton.SetActive(false);
        }

        /* update previous state */
        _playerWasInside = playerInside;

        if (_transitionButton.activeSelf && Input.GetKeyDown(KeyCode.E) && playerInside)
        {
            // Debug.Log("press E enter !");
            // if(GameManager.Instance != null) Debug.Log("game manager instance is not null ");
            // else Debug.Log("game manager instance is null QAQ ");
            // if(TransitionController.Instance != null) Debug.Log("transition controller instance is not null ");
            // else Debug.Log("transition controller instance is null QAQ ");
            TransitionController.Instance.TransitionToDestination(this);
            
        }
    }
}
