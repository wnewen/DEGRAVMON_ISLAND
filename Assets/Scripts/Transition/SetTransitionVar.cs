using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransitionVar : MonoBehaviour
{
    private TransitionPoint _transitionPoint;

    public void Start() 
    {
        _transitionPoint = GetComponent<TransitionPoint>();
    }

    public void SetClickTransitionButton()
    {
        _transitionPoint._clickTransitionButton = true;
    }
        
    
}
