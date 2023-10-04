using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* pick up item in scene */
public class ItemOnWorld : MonoBehaviour
{
    public Item _thisItem; // read by ObjectManager
    [SerializeField] private Inventory _playerInventory;

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Item"))
        {
            AddNewItem();
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    // private void OnTriggerEnter(Collider other) 
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         AddNewItem();
    //         Destroy(gameObject);
    //     }
    // }

    public void AddNewItem()
    {
        if (!_playerInventory._itemList.Contains(_thisItem))
        {
            _playerInventory._itemList.Add(_thisItem);
            EventHandler.CallAfterItemPickedEvent(this.gameObject, _playerInventory._itemList.Count - 1);
            Debug.Log("got item !");
        }
        else
        {
            Debug.Log("item + 1 !");
            _thisItem._itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
