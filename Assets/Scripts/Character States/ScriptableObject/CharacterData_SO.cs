using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character States/Data")]

public class CharacterData_SO : ScriptableObject
{
    [Header("State Info")]
    public int maxHealth; // 如果設成SerialField會無法被CharacterStates讀取
    public Transform position;
}
