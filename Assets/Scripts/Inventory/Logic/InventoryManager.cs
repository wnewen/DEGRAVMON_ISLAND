using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public ModelOnUI _modelOnUi;
    // [SerializeField] private GameObject _transitionButton;
    private bool _usedWatch;
    [SerializeField] private GameObject _watchButton;
    [SerializeField] private GameObject _watchButtonBack;
    [SerializeField] private GameObject _congradulationsUI;
    [SerializeField] private GameObject _bagIcon;
    [SerializeField] private GameObject _bookIcon;
    private bool _canOpenBag;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        _camera = GameObject.Find("Camera");
        _modelOnUi = _camera.GetComponent<ModelOnUI>();
    }

    private void OnEnable() 
    {
        RefreshItem();
        Instance._itemInformation.text = "";
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable() 
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnItemUsedEvent(Item item)
    {
        var index = GetItemIndex(item);
        Instance._myBag._itemList.RemoveAt(index);
        RefreshItem();

        if (Instance._myBag._itemList.Count == 0)
        {
            //empty myBag !
        }
    }

    private void Update() 
    {
        OpenMyBag();
    }
    
    private void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B) && _canOpenBag)
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
        Vector3 targetModelPosition = new Vector3(-1000, -1000 ,1.6f);


        /* delete exist model */
        GameObject existModel = GameObject.FindGameObjectWithTag("ItemModel");
        if (existModel != null) 
        {
            Destroy(existModel);
            Debug.Log("delete existModel");
            Debug.Log(existModel.name);
            // Destroy(Instance._modelOnUi);
            // Instance._camera.AddComponent(Instance._newScriptModelOnUi.GetType());
            // Instance._modelOnUi = Instance._camera.GetComponent<ModelOnUI>();
        }

        

        /* create new model */
        GameObject newModel = Instantiate(itemModel);
        /* set newModel positon, rotation, scale */
        newModel.transform.position = targetModelPosition; // instance position
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
        Vector3 desiredScale = new Vector3(1f, 1f, 1f); // set desired slot scale
        newItem.transform.localScale = desiredScale; 

        //可以添加Item Name
        newItem._slotItem = item;
        newItem._slotName.text = item._itemName;
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

    private int GetItemIndex(Item item)
    {
        for (int i = 0; i < Instance._myBag._itemList.Count; i++)
        {
            if (Instance._myBag._itemList[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void UseWatch()
    {
        _usedWatch = !_usedWatch;
        //TODO: after got fur watch broken
    }

    public static void ShowTransitionButton(string itemName)
    {
        if(itemName == "手錶")
        {
            if (Instance._usedWatch)
            {
                Instance._watchButtonBack.SetActive(true);
                Instance._watchButton.SetActive(false);
            }
                
            if (!Instance._usedWatch)
            {
                Instance._watchButton.SetActive(true);
                Instance._watchButtonBack.SetActive(false);
            }
        }
        else
        {
            //if (Instance._usedWatch)
                Instance._watchButtonBack.SetActive(false);
            //if (!Instance._usedWatch)
                Instance._watchButton.SetActive(false);
            
        }
    }

    public static void CheckPass(string itemName)
    {
        if(itemName == "銀狐們的靈魂")
        {
            Instance._congradulationsUI.SetActive(true);
        }
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        if (gameState == GameState.NewGame)
        {
            _myBag._itemList.Clear();
            RefreshItem();
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MainMenu")
        {
            _bagIcon.SetActive(false);
            _canOpenBag = false;
            _bookIcon.SetActive(false);
        }
        else
        {
            _bagIcon.SetActive(true);
            _canOpenBag = true;
            _bookIcon.SetActive(true);
        }
    }
}
