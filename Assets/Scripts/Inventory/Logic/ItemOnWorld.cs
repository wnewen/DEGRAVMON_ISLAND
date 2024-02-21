using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* pick up item in scene */
public class ItemOnWorld : MonoBehaviour
{
    [SerializeField] GameObject _Button;
    public Item _thisItem; // read by ObjectManager
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private float _detectionRadius = 3.0f;
    private bool _playerWasInside = false;


    // private void OnEnable()
    // {
    //     EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
    // }
    
    // private void OnDisable()
    // {
    //     EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
    // }
    private void Update() 
    {
        Vector3 _objectPosition = transform.position;

        // collect colliders in radius
        Collider[] _colliders = Physics.OverlapSphere(_objectPosition, _detectionRadius);

        bool _playerInside = false; // check whether player in range

        // check there is player in colliders
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _playerInside = true;
                break;
            }
        }

        if (_playerInside && !_playerWasInside)
        {
            _Button.SetActive(true);
        }
        else if (!_playerInside && _playerWasInside)
        {
            _Button.SetActive(false);
        }

        // update previous state
        _playerWasInside = _playerInside;

        if (_playerWasInside && Input.GetKeyDown(KeyCode.R))
        {
            AddNewItem();
            gameObject.SetActive(false);
            _Button.SetActive(false);
            EventHandler.CallAfterItemPickedEvent(this.gameObject, _playerInventory._itemList.Count - 1);
        }
    }

    public void AddNewItem()
    {
        if (!_playerInventory._itemList.Contains(_thisItem))
        {
            _playerInventory._itemList.Add(_thisItem);
            EventHandler.CallAfterItemPickedEvent(this.gameObject, _playerInventory._itemList.Count - 1);
            // Debug.Log("got item !");
        }
        else
        {
            // Debug.Log("item + 1 !");
            // _thisItem._itemHeld += 1;
        }

        InventoryManager.RefreshItem();
        InventoryManager.CheckPass(_thisItem._itemName);
    }

    // private void OnGameStateChangeEvent(GameState gameState)
    // {
    //     if (gameState == GameState.NewGame)
    //     {
    //         _playerInventory._itemList.Clear();
    //         InventoryManager.RefreshItem();
    //     }
    // }
}
