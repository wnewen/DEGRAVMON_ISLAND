using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* script on Prefab slot -> Prefab slot get item in scene info */
public class Slot : MonoBehaviour
{
    public Item _slotItem;
    public Image _slotImage;
    public GameObject _slotModel;
    public Text _slotNumber;

    public void ItemOnClicked()
    {
        Debug.Log("slot clicked");
        InventoryManager.UpdateItemInfo(_slotItem._itemInfo);
        InventoryManager.UpdateItemDetail(_slotItem._itemModel);
    }
}
