using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* define item instance format */
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string _itemName;
    public Sprite _itemImage;
    public GameObject _itemModel;
    public int _itemHeld;
    [TextArea]
    public string _itemInfo;
    // public bool _equip;
}
