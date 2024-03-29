using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private  DialogueData_SO _dialogueInitial;
    [SerializeField] private  DialogueData_SO _dialogueFinished;

    private Stack<string> _dialogueInitialStack;
    private Stack<string> _dialogueFinishedStack;

    private bool _isType;
    private Coroutine _dialogueRoutine;

    private void Awake() 
    {
        FillDialogueStack();
    }

    private void OnEnable()
    {
        EventHandler.DialogueTypeFinishedEvent += OnDialogueTypeFinishedEvent;
    }

    private void OnDisable()
    {
         EventHandler.DialogueTypeFinishedEvent -= OnDialogueTypeFinishedEvent;
    }

    private void OnDialogueTypeFinishedEvent()
    {
        _isType = false;
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
        StartCoroutine(DialogueRoutine(_dialogueInitialStack));        
    }

    public void ShowDialogueFinished()
    {
        StartCoroutine(DialogueRoutine(_dialogueFinishedStack));
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        if (_isType)
        {
            EventHandler.CallShowDialogueEvent("press again");
            yield return null;
            _isType = false;
        }
        else
        {
            if (data.TryPop(out string result))
            {
                _isType = true;
                EventHandler.CallShowDialogueEvent(result);
                yield return null;
            }
            else
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                FillDialogueStack();
            }
        }
    }
}
