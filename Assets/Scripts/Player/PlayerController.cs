using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private ControllerMovement3D _controllerMovement;
    private Vector3 _moveInput;
    private CharacterStates _characterStates;
    /* open & close bag GUI variables */
    // [SerializeField] private GameObject _myBag;
    // private bool _bagOpen;
    private Animator _anim;


    private void Awake() 
    {
        _controllerMovement = GetComponent<ControllerMovement3D>();
        _characterStates = GetComponent<CharacterStates>();
        // _myBag = GameObject.Find("Bag");
        _anim = GetComponent<Animator>();
    }

    private void Start() 
    {
        GameManager.Instance.RigisterPlayer(_characterStates);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0f , input.y);
    }

    public void OnJump(InputValue value)
    {
        _controllerMovement.Jump();
    }

    private void Update() {
        if (_controllerMovement == null) return;
        
        _controllerMovement.SetMovementInput(_moveInput);
        _controllerMovement.SetLookDirection(_moveInput);
        // OpenMyBag();
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        _anim.SetFloat("speed", _moveInput.magnitude);
    }
    // private void OpenMyBag()
    // {
    //     if (Input.GetKeyDown(KeyCode.B))
    //     {
    //         _bagOpen = !_bagOpen;
    //         _myBag.SetActive(_bagOpen);
    //     }
    // }
}
