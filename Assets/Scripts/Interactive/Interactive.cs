using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private Interactive instance;
    public Item _requiredItem;
    public bool _isDone;
    //public Inventory _myBag;
    private bool _congradulations;
    

    private void OnEnable() 
    {
        instance = this;
    }

    /* check if there is required item in myBag  */
    public virtual void CheckItem(string selectingItem)
    {
        //var itemList = instance._myBag._itemList;
        //for (int i = 0; i < itemList.Count; i++)
        //{
            if (selectingItem == _requiredItem._itemName)
            {
                _isDone = true;

                /* use item and remove item from bag */
                EventHandler.CallItemUsedEvent(_requiredItem);
            }
        //}
        InteractingAction();
    }

    public virtual void InteractingAction()
    {
        Debug.Log("做動作");
    }
    
}
