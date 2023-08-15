using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    [SerializeField] private CharacterData_SO characterData;

    #region  Read from Data_SO
    public int maxHealth 
    { 
        get { if (characterData != null) return characterData.maxHealth; else return 0;}
        set { characterData.maxHealth = value; }
    }
    #endregion
}
