using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchSystem : MonoBehaviour
{
    [SerializeField] string _sceneFrom;
    [SerializeField] string _sceneToGo;
    private GameObject _player;

    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // public void SwitchScene()
    // {
    //     SavePlayerPosition();
    //     TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    // }

    // // save palyer positon before switch scene
    // private void SavePlayerPosition()
    // {
    //     PlayerPositionManager.SavePlayerPosition(_player.transform.position);
    // }
}
