using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private Interactive instance;
    public Item _requiredItem;
    public bool _isDone;
    public Inventory _myBag;
    

    private void OnEnable() 
    {
        instance = this;
    }

    /* check if there is required item in myBag  */
    public void CheckItem()
    {
        var itemList = instance._myBag._itemList;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == _requiredItem)
            {
                _isDone = true;
                
                /* use item and remove item from bag */
                EventHandler.CallItemUsedEvent(_requiredItem);
            }
            
        }
        InteractingAction();
    }

    protected virtual void InteractingAction()
    {
        Debug.Log("做動作");
    }
    
}
