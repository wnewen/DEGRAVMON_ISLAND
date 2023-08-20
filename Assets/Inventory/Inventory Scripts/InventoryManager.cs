using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* deal with items in world & item not instantiate */
public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory _myBag;
    public GameObject _slotGrid;
    public Slot _slotPrefab;
    public Text _itemInformation;

    void Awake() 
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable() {
        RefreshItem();
        instance._itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance._itemInformation.text = itemDescription;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance._slotPrefab, instance._slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance._slotGrid.transform); // newItem became child of bagGrid
        
        newItem._slotItem = item;
        newItem._slotImage.sprite = item._itemImage;
        newItem._slotModel = item._itemModel;
        // newItem._slotNumber.text = item._itemHeld.ToString();
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance._slotGrid.transform.childCount; i++)
        {
            if (instance._slotGrid.transform.childCount == 0)
                break;
            Destroy(instance._slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance._myBag._itemList.Count; i++)
        {
            CreateNewItem(instance._myBag._itemList[i]);
        }
    }
}
