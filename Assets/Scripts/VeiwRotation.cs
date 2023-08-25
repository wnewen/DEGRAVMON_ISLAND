using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeiwRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5.0f;
    private Camera _camera;
    private Transform _cameraTransform;
    private float _targetRotation = 0.0f;
    private bool _isRotating = false;

    private void Awake() 
    {
        _camera = GameObject.FindObjectOfType<Camera>();
        _cameraTransform = _camera.transform;
    }

    void Update()
    {
        if (_isRotating)
        {
            /* use Lerp to rotate camera smooth */
            float step = _rotationSpeed * Time.deltaTime;
            float currentRotation = _cameraTransform.eulerAngles.y;
            _cameraTransform.eulerAngles = new Vector3(0, Mathf.Lerp(currentRotation, _targetRotation, step), 0);

            /* stop rotation */
            if (Mathf.Abs(currentRotation - _targetRotation) < 1.0f)
            {
                _isRotating = false;
            }
        }
    }    

    public void RotateCounterClockwise()
    {
        if (!_isRotating)
        {
            _targetRotation = _cameraTransform.eulerAngles.y - 90.0f;
            _isRotating = true;
        }
    }

    public void RotateClockwise()
    {
        if (!_isRotating)
        {
            _targetRotation = _cameraTransform.eulerAngles.y + 90.0f;
            _isRotating = true;
        }
    }
}