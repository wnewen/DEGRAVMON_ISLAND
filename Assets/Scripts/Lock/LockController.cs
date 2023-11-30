using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockController : MonoBehaviour
{
    [SerializeField] private GameObject _cabinet;
    [SerializeField] private GameObject _lockUI;
    [SerializeField] private GameObject _hint;
    [SerializeField] private GameObject _numberOnLock01;
    [SerializeField] private GameObject _numberOnLock02;
    [SerializeField] private GameObject _numberOnLock03;
    private  bool _isCorrect;
    public void CheckPassword()
    {
        _isCorrect = true;
        if (_numberOnLock01.GetComponent<Text>().text != "5")
            _isCorrect = false;
        if (_numberOnLock02.GetComponent<Text>().text != "2")
            _isCorrect = false;
        if (_numberOnLock03.GetComponent<Text>().text != "1")
            _isCorrect = false;

        if (_isCorrect)
        {
            Debug.Log("password correct !");
            _cabinet.GetComponent<Interactive>()._isDone = true;
            Destroy(_lockUI);
        }
        else 
            _hint.SetActive(true);
    }
}
