using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneButton : MonoBehaviour
{
    [SerializeField] private  GameObject _Button;
    [SerializeField] private float _detectionRadius = 5.0f;
    private bool _playerWasInside = false;
    

    private void Update() 
    {
        Vector3 _buttonPosition = transform.position;

        // collect colliders in radius
        Collider[] _colliders = Physics.OverlapSphere(_buttonPosition, _detectionRadius);

        bool _playerInside = false; // check whether player in range

        // check there is player in colliders
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _playerInside = true;
                Debug.Log("player is detected");
                break;
            }
        }

        // player enter the detection range
        if (_playerInside && !_playerWasInside)
        {
            Debug.Log("Player entered the detection zone!");
            _Button.SetActive(true);
        }
        // player exit the detection range
        else if (!_playerInside && _playerWasInside)
        {
            Debug.Log("Player exited the detection zone!");
            _Button.SetActive(false);
        }

        // update previous state
        _playerWasInside = _playerInside;

        if (_Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("press E enter the stone house !");
        }
    }
}
