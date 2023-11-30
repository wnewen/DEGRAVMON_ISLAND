using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetInOffice : Interactive
{
    private DialogueController _dialogueController;
    private bool _playerGotToken;
    [SerializeField] private GameObject _LockUI;
    private bool _isLockUIOpen;

    
    private void Awake() 
    {
        _dialogueController = GetComponent<DialogueController>();
    }

    protected override void InteractingAction()
    {
        if (_isDone)
        {
            _dialogueController.ShowDialogueFinished();
            //player got watch == add watch to player's bag
        }
        else
        {
            if(!_playerGotToken)
            {
                //give player a token == add token to player's bag
                
                _playerGotToken = true;
            }
            _dialogueController.ShowDialogueInitial();
            //show ui to input the password
            _isLockUIOpen = !_isLockUIOpen;
            _LockUI.SetActive(_isLockUIOpen);
        }
            
    }
}
