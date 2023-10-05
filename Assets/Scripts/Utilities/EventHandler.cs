using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<GameObject, int> AfterItemPickedEvent;
    public static void CallAfterItemPickedEvent(GameObject item, int index)
    {
        Debug.Log("suceed to CallAfterItemPickedEvent");
        AfterItemPickedEvent?.Invoke(item, index);
    }
}