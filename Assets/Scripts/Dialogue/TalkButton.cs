using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [SerializeField] private  GameObject _Button;
    [SerializeField] private  GameObject _talkUI;
    [SerializeField] private float _detectionRadius = 5.0f;
    private bool _playerWasInside = false;

    [SerializeField] private Item _interactiveItem;

    private void Update() 
    {
        Vector3 _objectPosition = transform.position;

        // collect colliders in radius
        Collider[] _colliders = Physics.OverlapSphere(_objectPosition, _detectionRadius);

        bool _playerInside = false; // check whether player in range

        // check there is player in colliders
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _playerInside = true;
                break;
            }
        }

        // player enter the detection range
        if (_playerInside && !_playerWasInside)
        {
            _Button.SetActive(true);
        }
        // player exit the detection range
        else if (!_playerInside && _playerWasInside)
        {
            _Button.SetActive(false);
            _talkUI.SetActive(false);
        }

        // update previous state
        _playerWasInside = _playerInside;

        if (_Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            _talkUI.SetActive(true);
            
            var interactive = this.gameObject.GetComponent<Interactive>();
            if (interactive != null)
            {
                interactive.CheckItem();
            }
            else{
                Debug.Log("did not get interactive");
            }
        }
    }

}


