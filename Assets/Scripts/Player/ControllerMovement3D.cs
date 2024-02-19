using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMovement3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _turnSpeed = 50f;
    public GameObject _mainCamera; // 須被 GameManager 讀取
    private float _speed = 0f;
    private bool _hasMoveInput;
    private Vector3 _moveInput;
    private Vector3 _lookDirection;

    [Header("Jumping")]
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _jumpHeight = 1.5f;
    private Vector3 _velocity;

    [Header("Grounding")]
    [SerializeField] private float _groundCheckOffset = 0.35f;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private float _groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isGrounded;
    private Vector3 _groundNormal;
    private AudioSource _playerWalkingAudio;
    private Scene _currentScene;


    private CharacterController _characterController; 

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerWalkingAudio = GetComponent<AudioSource>();
        _currentScene = SceneManager.GetActiveScene();
    }
    
    public void SetMovementInput(Vector3 input)
    {
        // check if the player press the key or not
        _hasMoveInput = input.magnitude > 0.1f;
        _moveInput = _hasMoveInput ? input : Vector3.zero;

    }

    public void SetLookDirection(Vector3 direction)
    {
        // rotate the player
        _lookDirection = new Vector3(direction.x, 0f, direction.z).normalized;

    }

    public void Jump()
    {
        // check if it grounded
        if(!_isGrounded) return;

        // calculate jump velocity from jump height and gravity
        float jumpVelocity = Mathf.Sqrt(2f * -_gravity * _jumpHeight);
        _velocity = new Vector3(0,jumpVelocity, 0);
    }

    private void FixedUpdate() 
    {
        _isGrounded = CheckGround();

        //jumping
        _velocity.y += _gravity * Time.fixedDeltaTime;
        _characterController.Move(_velocity * Time.fixedDeltaTime);
        

        _speed = 0;
        float targetRotation = 0f;

        if(_moveInput.magnitude < 0.1f)
        {
            _moveInput = Vector3.zero;
            _playerWalkingAudio.Pause();
            return;
        }

        // move character
        if(_moveInput != Vector3.zero)
        {
            _speed = _moveSpeed;
            if (!_playerWalkingAudio.isPlaying && (_currentScene.name == "GrassHill01"))
            {
                _playerWalkingAudio.Play();
            }
        }

        targetRotation = Quaternion.LookRotation(_lookDirection).eulerAngles.y + _mainCamera.transform.rotation.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnSpeed * Time.fixedDeltaTime); // smooth the rotation

        _moveInput = rotation * Vector3.forward;
        _characterController.Move(_moveInput * _speed * Time.fixedDeltaTime);
    }

    private bool CheckGround()
    {
        // find start position
        Vector3 start = transform.position + Vector3.up * _groundCheckOffset;

        // perfrom spherecast
        if(Physics.SphereCast(start, _groundCheckRadius, Vector3.down, out RaycastHit hit, _groundCheckDistance, _groundMask))
        {
            return true;
        }
        return false;
    }

    // debug sphere
    private void OnDrawGizmosSelected()
    {
        // set gizmos color
        Gizmos.color = Color.red;
        if(_isGrounded) Gizmos.color = Color.green;

        // find start/end positon of sphere cast
        Vector3 start = transform.position + Vector3.up * _groundCheckOffset;
        Vector3 end = start + Vector3.down * _groundCheckDistance;

        // draw wire spheres
        Gizmos.DrawWireSphere(start, _groundCheckRadius);
        Gizmos.DrawWireSphere(end, _groundCheckRadius);
    }

}
