using System;
using UnityEngine;

public class PlayerInput
{
    public Transform Pivot => _pivot;
    public Transform Point => _point;

    private Transform _pivot;
    private Transform _point;
    private Transform _forward;
    
    private Vector3 _inputAxis = Vector3.zero;
    private Vector3 _position = Vector3.zero;
    
    private Vector3 _rotationAxis = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _forwardAxis = Vector3.zero;
    private float _scrollValue = 0f;

    private InputSettings.InputData _settings;
    private PlayerCamera _camera;
    
    public PlayerInput(InputSettings.InputData settings, PlayerCamera camera, Transform root)
    {
        _settings = settings;
        _camera = camera;

        _pivot = new GameObject("Pivot").transform;
        _pivot.SetParent(root);
        
        _point = new GameObject("Point").transform;
        _point.SetParent(_pivot);
        
        _forward = new GameObject("Forward").transform;
        _forward.SetParent(root);
    }

    public void Execute()
    {
        DoMove();
        DoRotate();
    }

    private void DoMove()
    {
        _inputAxis.x = Input.GetAxis("Horizontal") * _settings.SpeedMove * Time.deltaTime;
        _inputAxis.z = Input.GetAxis("Vertical") * _settings.SpeedMove * Time.deltaTime;

        _position = Vector3.ClampMagnitude(_position + _forward.TransformDirection(_inputAxis), _settings.ClampAreaMovement);

        _point.localPosition = _position;
    }
    
    private void DoRotate()
    {
        if (Input.GetMouseButton(1))
        {
            SetCurcor(CursorMode.Rotate);
                
            _rotationAxis.x = Input.GetAxis("Mouse X") * _settings.RotateHorizontal * Time.deltaTime;
            _rotationAxis.z = -Input.GetAxis("Mouse Y") * _settings.RotateVertical * Time.deltaTime;

            _rotation.y += _rotationAxis.x;
            _rotation.x = Mathf.Clamp(_rotation.x + _rotationAxis.z, _settings.ClampMinVertical, _settings.ClampMaxVertical);

            _forwardAxis.y = _rotation.y;
                
            _point.localRotation = Quaternion.Euler(_rotation);
            _forward.localRotation = Quaternion.Euler(_forwardAxis);
        }
        else
        {
            SetCurcor(CursorMode.Free);
        }

        _scrollValue += Input.mouseScrollDelta.y * _settings.SpeedScroll * Time.deltaTime;
        _scrollValue = Mathf.Clamp(_scrollValue, _settings.ScrollMinDistance, _settings.ScrollMaxDistance);
        _camera.SetHeight(-_scrollValue);
    }

    private void SetCurcor(CursorMode mode)
    {
        switch (mode)
        {
            case CursorMode.Free:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            
            case CursorMode.Rotate:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }
    
    private enum CursorMode
    {
        Free,
        Rotate,
    }
}