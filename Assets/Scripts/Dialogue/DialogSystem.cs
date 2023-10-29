using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogSystem : MonoBehaviour
{
    [Header("UI_component")]
    [SerializeField] private Text _textContent;
    [SerializeField] private Image _characterImage;

    [Header("TextFile")]
    [SerializeField] private TextAsset _textFile;
    [SerializeField] private int _textFileLineIndex;
    [SerializeField] private float _typeSpeed = 0.1f;

    [Header("FaceImage")]
    [SerializeField] private Sprite _characterFox, _characterMain;

    // type text
    private bool _typeFinished; 
    private bool _cancelTyping;

    List<string> _textList = new List<string>();


    private void Awake() {
        if(_textFile == null) return;

        GetTextFromFile(_textFile);
    }

    private void OnEnable() 
    {
        _typeFinished = true;
        StartCoroutine(SetTextUI());   
    }

    private void Update() 
    {
        // print last text line & reset index
        if(Input.GetKeyDown(KeyCode.R) && _textFileLineIndex == _textList.Count)
        {
            gameObject.SetActive(false);
            _textFileLineIndex = 0;
            return;
        }

        // simple text typing system state check
        // if(Input.GetKeyDown(KeyCode.R) && _typeFinished)
        // {
        //     StartCoroutine(SetTextUI());
        // }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(_typeFinished && !_cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!_typeFinished && !_cancelTyping)
            {
                _cancelTyping = true;
            }
        }
    }

    private void GetTextFromFile(TextAsset file)
    {
        Debug.Log("getting text file");
        _textList.Clear();
        _textFileLineIndex = 0;

        var _textFileLineContent = file.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        Debug.Log("line content : " + _textFileLineContent);

        foreach (var line in _textFileLineContent)
        {
            _textList.Add(line);
            Debug.Log(_textList);
        }
    }

    IEnumerator SetTextUI()
    {
        _typeFinished = false;
        _textContent.text = "";

        switch(_textList[_textFileLineIndex])
        {
            case "A" :
                _characterImage.sprite = _characterFox;
                _textFileLineIndex ++;
                break;
            case "B" :
                _characterImage.sprite = _characterMain;
                _textFileLineIndex ++;
                break;
            default:
                break;
        }

        // simple typing system(cannot overview complete sentence)
        // for (int i = 0; i < _textList[_textFileLineIndex].Length; i++)
        // {
        //     _textContent.text += _textList[_textFileLineIndex][i];

        //     yield return new WaitForSeconds(_typeSpeed);
        // }

        // complete typing system
        int letter = 0;
        while(!_cancelTyping && letter < _textList[_textFileLineIndex].Length - 1)
        {
            _textContent.text += _textList[_textFileLineIndex][letter];
            letter ++;
            yield return new WaitForSeconds(_typeSpeed);
        }
        _textContent.text = _textList[_textFileLineIndex];
        _cancelTyping = false;
        _typeFinished = true;
        _textFileLineIndex ++;
    }
}
