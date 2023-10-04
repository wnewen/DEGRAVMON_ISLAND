using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* deal with items in world & item not instantiate */
public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory _myBag;
    [SerializeField] private GameObject _myBagUi;
    private bool _bagOpen;
    public GameObject _slotGrid;
    public Slot _slotPrefab;
    [Header("Item Info")]
    public Text _itemInformation;
    [Header("item Detail")]
    public RawImage _itemDetail;
    public GameObject _camera;
    public ModelOnUi _modelOnUi;
    // public ModelOnUi _newScriptModelOnUi;

   
    // void Awake() 
    // {
    //     if (instance != null)
    //         Destroy(this);
    //     instance = this;
    // }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        _camera = GameObject.Find("Camera");
        _modelOnUi = _camera.GetComponent<ModelOnUi>();
    }

    private void OnEnable() {
        RefreshItem();
        Instance._itemInformation.text = "";
    }

    private void Update() 
    {
        OpenMyBag();
    }
    
    private void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _bagOpen = !_bagOpen;
            _myBagUi.SetActive(_bagOpen);
        }
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        Instance._itemInformation.text = itemDescription;
    }

    /* exhibit item model */
    public static void UpdateItemDetail(GameObject itemModel)
    {
        /* set itemModel postion */
        Vector3 targetModelPosition = new Vector3(-1000, 0 ,1.6f);


        /* delete exist model */
        GameObject existModel = GameObject.FindGameObjectWithTag("ItemModel");
        if (existModel != null) 
        {
            Debug.Log("delete existModel");
            // Destroy(existModel);
            // Destroy(Instance._modelOnUi);
            // Instance._camera.AddComponent(Instance._newScriptModelOnUi.GetType());
            // Instance._modelOnUi = Instance._camera.GetComponent<ModelOnUi>();
        }

        

        /* create new model */
        GameObject newModel = Instantiate(itemModel);
        /* set newModel positon, rotation, scale */
        // newModel.transform.position = targetModelPosition; // instance position
        // newModel.transform.rotation = Quaternion.identity; // no rotation
        // newModel.transform.localScale = new Vector3(6, 6, 6); // no scale
        Instance._modelOnUi.pivot = newModel.transform;
        Instance._modelOnUi.targetX = 0f;
        Instance._modelOnUi.targetY = 0f;
        Instance._modelOnUi.targetDistance = 10f;
        
    }

    public static void CreateNewItem(Item item)
    {
        /* create newItem by prefab, set newItem position & rotation */
        Slot newItem = Instantiate(Instance._slotPrefab, Instance._slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(Instance._slotGrid.transform); // newItem became child of bagGrid
        
        //可以添加Item Name
        newItem._slotItem = item;
        newItem._slotImage.sprite = item._itemImage;
        newItem._slotModel = item._itemModel;
        newItem._slotNumber.text = item._itemHeld.ToString();
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < Instance._slotGrid.transform.childCount; i++)
        {
            if (Instance._slotGrid.transform.childCount == 0)
                break;
            Destroy(Instance._slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Instance._myBag._itemList.Count; i++)
        {
            CreateNewItem(Instance._myBag._itemList[i]);
        }
    }
}
