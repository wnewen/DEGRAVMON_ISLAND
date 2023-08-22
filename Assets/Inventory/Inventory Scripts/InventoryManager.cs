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
    [Header("Item Info")]
    public Text _itemInformation;
    [Header("item Detail")]
    public RawImage _itemDetail;
    public GameObject _camera;
    public ModelOnUi _modelOnUi;


    
    
   
    void Awake() 
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start() 
    {
        _camera = GameObject.Find("Camera");
        _modelOnUi = _camera.GetComponent<ModelOnUi>();
    }

    private void OnEnable() {
        RefreshItem();
        instance._itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance._itemInformation.text = itemDescription;
    }

    /* exhibit item model */
    public static void UpdateItemDetail(GameObject itemModel)
    {
        /* set itemModel postion */
        Vector3 targetModelPosition = new Vector3(-1000, 0 ,1.6f);

        /* delete exist model */
        GameObject existModel = GameObject.FindGameObjectWithTag("ItemModel");
        if (existModel != null) Destroy(existModel);

        /* create new model */
        GameObject newModel = Instantiate(itemModel);
        /* set newModel positon, rotation, scale */
        newModel.transform.position = targetModelPosition; // instance position
        newModel.transform.rotation = Quaternion.identity; // no rotation
        newModel.transform.localScale = new Vector3(6, 6, 6); // no scale
        instance._modelOnUi.pivot = newModel.transform;
        
    }

    public static void CreateNewItem(Item item)
    {
        /* create newItem by prefab, set newItem position & rotation */
        Slot newItem = Instantiate(instance._slotPrefab, instance._slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance._slotGrid.transform); // newItem became child of bagGrid
        
        newItem._slotItem = item;
        newItem._slotImage.sprite = item._itemImage;
        newItem._slotModel = item._itemModel;
        newItem._slotNumber.text = item._itemHeld.ToString();
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
