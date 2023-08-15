using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStates _playerStates; //待確認：是否必須要public

    public void RigisterPlayer(CharacterStates player)
    {
        _playerStates = player;
    }
}
