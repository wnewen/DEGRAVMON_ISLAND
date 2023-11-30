using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNumber : MonoBehaviour
{
    [SerializeField] private GameObject _numberOnLock;
    private int _number;

    public void PlusNumber()
    {
        _number = (int.Parse(_numberOnLock.GetComponent<Text>().text) + 1) % 10;
        _numberOnLock.GetComponent<Text>().text = _number.ToString();
    }
}
