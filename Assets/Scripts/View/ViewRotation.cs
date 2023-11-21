using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private Camera _camera;
    private Transform _cameraTransform;
    private float _targetRotation = 0.0f;
    private float _targetAngle = 0.0f;
    [SerializeField] private bool _isRotating;

    float[] _rotationList = new float[] { 30f, 120f, 210f, 360f };
    public int _rotationCounter = 0;
    
    private GameObject _left;
    private GameObject _right;
    private GameObject _toPrevious;
    private GameObject _toNext;

    private void Awake() 
    {
        // _camera = GameObject.FindObjectOfType<Camera>();
        _cameraTransform = _camera.transform;

        _left = GameObject.Find("Left");
        _right = GameObject.Find("Right");
        _toPrevious = GameObject.Find("PreviousDoor");
        _toNext = GameObject.Find("NextDoor");
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

        switch (_rotationCounter)
        {
            case 0:
                _left.SetActive(false);
                _toPrevious.SetActive(true);
                break;
            case 3:
                _right.SetActive(false);
                _toNext.SetActive(true);
                break;
            default:
                _left.SetActive(true);
                _right.SetActive(true);
                _toPrevious.SetActive(false);
                _toNext.SetActive(false);
                break;
        }
    }   

    public void RotateCounterClockwise()
    {
        if (!_isRotating)
        {
            _rotationCounter = _rotationCounter - 1;
            _targetAngle = _rotationList[_rotationCounter];
            _targetRotation = _targetAngle;
            _isRotating = true;
        }
    }

    public void RotateClockwise()
    {
        if (!_isRotating)
        {
            _rotationCounter = _rotationCounter + 1;
            _targetAngle = _rotationList[_rotationCounter];
            _targetRotation = _targetAngle;
            _isRotating = true;
        }
    }
}