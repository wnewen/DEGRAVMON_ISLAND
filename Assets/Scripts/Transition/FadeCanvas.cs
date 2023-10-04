using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : Singleton<FadeCanvas>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
