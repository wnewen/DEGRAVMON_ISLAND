using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetInOffice : Interactive
{
    // private DialogueController _dialogueController;
    private bool _playerGotToken;
    [SerializeField] private GameObject _LockUI;
    [SerializeField] private GameObject _watch;
    private bool _isLockUIOpen;

    
    private void Awake() 
    {
        // _dialogueController = GetComponent<DialogueController>();
        // _LockUI =
        // _watch = GameObject.Find("Watch");
    }

    public override void InteractingAction()
    {
        if (_isDone)
        {
            // _dialogueController.ShowDialogueFinished();
            //TODO: Open the cabinet
            _watch.SetActive(true);
            _isLockUIOpen = !_isLockUIOpen;
            _LockUI.SetActive(_isLockUIOpen);
        }
        else
        {
            if(!_playerGotToken)
            {
                _playerGotToken = true;
            }
            //_dialogueController.ShowDialogueInitial();

            /* show ui to input the password */
            _isLockUIOpen = !_isLockUIOpen;
            _LockUI.SetActive(_isLockUIOpen);
        }
            
    }
}
