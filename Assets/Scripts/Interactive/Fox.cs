using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Interactive
{
    private void OnEnable() 
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable() 
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        if (!_isDone)
        {
            // object still not get the correct item and the object is active   
        }
        else
        {
            //object got the correct item and the object is not acitve
        }
    }

    protected override void OnClickAction()
    {
        //do the action
    }
}
