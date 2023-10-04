using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<string, bool> _itemAvailableDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.AfterItemPickedEvent += OnAfterItemPickedEvent;
    }

    private void OnDisable() 
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.AfterItemPickedEvent -= OnAfterItemPickedEvent;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (var item in items)
        {
            // ItemOnWorld itemOnWorld;
            // itemOnWorld = item.GetComponent<ItemOnWorld>();

            if (!_itemAvailableDict.ContainsKey(item.name))
                _itemAvailableDict.Add(item.name, true);
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (var item in items)
        {
            // ItemOnWorld itemOnWorld;
            // itemOnWorld = item.GetComponent<ItemOnWorld>();

            if (!_itemAvailableDict.ContainsKey(item.name))
                _itemAvailableDict.Add(item.name, true);
            else
                item.gameObject.SetActive(_itemAvailableDict[item.name]);          
        }
    }

    private void OnAfterItemPickedEvent(GameObject item, int index)
    {
        if (item != null)
        {
            _itemAvailableDict[item.name] = false;
        }
    }
}
