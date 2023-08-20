using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* pick up item in scene */
public class ItemOnWorld : MonoBehaviour
{
    [SerializeField] private Item _thisItem;
    [SerializeField] private Inventory _playerInventory;

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Collection"))
        {
            AddNewItem();
            Destroy(gameObject);
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
            Debug.Log("got item !");
        }
        else
        {
            Debug.Log("did not get item QAQ");
            // _thisItem._itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
