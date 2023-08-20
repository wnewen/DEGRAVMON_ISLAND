using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* items in bag */
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> _itemList = new List<Item>();
}
