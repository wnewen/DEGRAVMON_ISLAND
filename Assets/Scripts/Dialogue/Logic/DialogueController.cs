using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private  DialogueData_SO _dialogueInitial;
    [SerializeField] private  DialogueData_SO _dialogueFinished;

    private Stack<string> _dialogueInitialStack;
    private Stack<string> _dialogueFinishedStack;

    private bool _isTalking;

    private void Awake() 
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        _dialogueInitialStack = new Stack<string>();
        _dialogueFinishedStack = new Stack<string>();

        for (int i = _dialogueInitial._dialogueList.Count - 1; i > -1; i--)
        {
            _dialogueInitialStack.Push(_dialogueInitial._dialogueList[i]);
        }
        for (int i = _dialogueFinished._dialogueList.Count - 1; i > -1; i--)
        {
            _dialogueFinishedStack.Push(_dialogueFinished._dialogueList[i]);
        }
    }

    public void ShowDialogueInitial()
    {
        if (!_isTalking)
            StartCoroutine(DialogueRoutine(_dialogueInitialStack));
    }

    public void ShowDialogueFinished()
    {
        if (!_isTalking)
            StartCoroutine(DialogueRoutine(_dialogueFinishedStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        _isTalking = true;
        if (data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            _isTalking = false;
        }
        else
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            _isTalking = false;
        }
    }

}
