using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickedUI : MonoBehaviour
{
    [SerializeField] private GameObject _itemPickedUI;
    [SerializeField] private Text _itemPickedText;
    private float _displayTime = 1.0f;
    private bool _isDisplay;

    private void OnEnable()
    {
        EventHandler.AfterItemPickedEvent += OnAfterItemPickedEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterItemPickedEvent -= OnAfterItemPickedEvent;
    }

    // private void Start()
    // {
    //     _itemPickedText = 
    // }

    private void OnAfterItemPickedEvent(GameObject item, int index)
    {
        ItemOnWorld itemScript = item.GetComponent<ItemOnWorld>();
        string itemName = itemScript._thisItem._itemName;
        DisplayItemPickedText("獲得了 " + itemName + " !");
        Invoke("HideItemPickedText", _displayTime);
    }

    private void DisplayItemPickedText(string message)
    {
        _itemPickedText.text = message;
        _itemPickedUI.SetActive(true);
        // _isDisplay = true;
    }

    private void HideItemPickedText()
    {
        _itemPickedUI.SetActive(false);
        // _isDisplay = false;
    }
}
