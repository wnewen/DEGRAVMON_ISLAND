using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLandslideIncident : Interactive
{
    private DialogueController _dialogueController;
    [SerializeField] private GameObject _soul;
    // [SerializeField] private GameObject _congradulationsUI;

    
    private void Awake() 
    {
        _dialogueController = GetComponent<DialogueController>();
    }

    protected override void InteractingAction()
    {
        if (_isDone)
        {
            _dialogueController.ShowDialogueFinished();
            _soul.SetActive(true);
        }
        else
            _dialogueController.ShowDialogueInitial();
    }
}
