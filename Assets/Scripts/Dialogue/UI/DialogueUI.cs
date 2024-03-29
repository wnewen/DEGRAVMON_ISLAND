using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    private string _typeContent = "";
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Text _dialogueText;
    private Coroutine _typeCoroutine;
    private float _typeSpeed = 0.1f;
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

        if (_typeCoroutine != null)
        {
            StopCoroutine(_typeCoroutine);
        }
        _typeCoroutine = StartCoroutine(TypeDialogue(dialogue));
    }

    private IEnumerator TypeDialogue(string dialogue)
    {   
        _dialogueText.text = "";
        if (dialogue != "press again")
        {
            _typeContent = dialogue;
            for (int letter = 0; letter < _typeContent.Length; letter++)
            {
                _dialogueText.text += _typeContent[letter];
                yield return new WaitForSeconds(_typeSpeed);
                if (letter == _typeContent.Length-1)
                {
                    EventHandler.CallDialogueTypeFinishedEvent();
                    Debug.Log("suceed");
                }   
            }
        }
        else 
        {
            _dialogueText.text = _typeContent;
            yield return null;
        }
    }
}
