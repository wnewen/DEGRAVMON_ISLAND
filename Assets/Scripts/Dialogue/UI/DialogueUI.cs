using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Text _dialogueText;

    private void OnEnable() 
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }

    private void OnDisable() 
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
    }
    private void ShowDialogue(string dialogue)
    {
        if (dialogue != string.Empty)
            _dialogueUI.SetActive(true);
        else
            _dialogueUI.SetActive(false);
        _dialogueText.text = dialogue;
    }
}
