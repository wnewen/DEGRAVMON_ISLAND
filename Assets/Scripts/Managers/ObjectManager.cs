using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private Dictionary<string, bool> _itemAvailableDict = new Dictionary<string, bool>();
    private Dictionary<string, bool> _interactiveStateDict = new Dictionary<string, bool>();


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    
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
            if (!_itemAvailableDict.ContainsKey(item.name))
                _itemAvailableDict.Add(item.name, true);
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (_interactiveStateDict.ContainsKey(item.name))
                _interactiveStateDict[item.name] = item._isDone;
            else
                _interactiveStateDict.Add(item.name, item._isDone);
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (var item in items)
        {
            if (!_itemAvailableDict.ContainsKey(item.name))
                _itemAvailableDict.Add(item.name, true);
            else
                item.gameObject.SetActive(_itemAvailableDict[item.name]);    
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (_interactiveStateDict.ContainsKey(item.name))
                item._isDone = _interactiveStateDict[item.name];
            else
                _interactiveStateDict.Add(item.name, item._isDone);  
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
