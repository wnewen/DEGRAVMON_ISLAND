using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLandslideIncident : Interactive
{
    private DialogueController _dialogueController;

    
    private void Awake() 
    {
        _dialogueController = GetComponent<DialogueController>();
    }

    protected override void InteractingAction()
    {
        if (_isDone)
            _dialogueController.ShowDialogueFinished();
        else
            _dialogueController.ShowDialogueInitial();
    }
}
