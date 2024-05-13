using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButtonController : MonoBehaviour
{
    private Transform _buttonParent;
    [SerializeField] private GameObject _itemUseButtonObj;
    private Button _itemUseButton;
    private float _detectionRadius = 5.0f;
    private bool _bagEmpty;
    private Interactive _NPCScript;
    private bool _NPCInside; // check whether player in range
    private string _selectingItem;
    [SerializeField] Inventory _myBag;

    private void OnEnable()
    {
        EventHandler.ItemSelectingEvent += OnItemSelectingEvent;
        //EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectingEvent -= OnItemSelectingEvent;
        //EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
    }

    private void Update()
    {
        if(!_buttonParent)
        {
            _buttonParent = GameObject.Find("Button").transform;
            if (!_itemUseButtonObj)
            {
                _itemUseButtonObj = _buttonParent.GetChild(0).gameObject;
                _itemUseButton = _itemUseButtonObj.GetComponent<Button>();
                _itemUseButton.onClick.AddListener(OnButtonClick);
            }
        }
        

        if (_myBag._itemList.Count != 0)
        {
            _bagEmpty = false;
        }
            
        else
            _bagEmpty = true;

        Vector3 _objectPosition = transform.position;

        // collect colliders in radius
        Collider[] _colliders = Physics.OverlapSphere(_objectPosition, _detectionRadius);       

        // check there is player in colliders
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                _NPCInside = true;
                _NPCScript = collider.GetComponent<Interactive>();
                break;
            }
            else
            {
                _NPCInside = false;
                _NPCScript = null;
            }
        }

        if(_itemUseButton)
        {
            if (!_bagEmpty && _NPCInside)
            {
                _itemUseButtonObj.SetActive(true);
            }
            else 
            {
                _itemUseButtonObj.SetActive(false);
            }
        }
        
    }


    

    private void OnItemSelectingEvent(string selectingItem)
    {
        _selectingItem = selectingItem;
    }
    private void OnButtonClick()
    {
        _NPCScript.CheckItem(_selectingItem);
    }

    //private void OnBeforeSceneUnloadEvent()
    //{
    //    _itemUseButtonObj.SetActive(true);
    //    _itemUseButtonObj = null;
    //}
}