using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    // [SerializeField] private Button _leftButton, _rightButton;

    [SerializeField] private int _currentItemIndex;

    private void OnEnable() 
    {
        EventHandler.AfterItemPickedEvent += OnAfterItemPickedEvent;   
    }

    private void OnDisable() 
    {
        EventHandler.AfterItemPickedEvent -= OnAfterItemPickedEvent;   
    }

    private void OnAfterItemPickedEvent(GameObject item, int index)
    {
        if (item == null)
        {
            _currentItemIndex = -1;
            // _leftButton.interactable = false;
            // _rightButton.interactable = false;
        }
        else
        {
            _currentItemIndex = index;
        }
    }
}
