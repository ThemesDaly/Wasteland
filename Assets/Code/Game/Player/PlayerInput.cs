using System;
using UnityEngine;

public class PlayerInput
{
    public Action<Vector3> OnEventClick;
    
    private Transform _pivot;
    private Transform _point;
    private Transform _forward;
    
    private Vector3 inputAxis = Vector3.zero;
    private Vector3 position = Vector3.zero;
    
    private Vector3 rotationAxis = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private float scrollValue = 0f;

    private Ray ray;
    private RaycastHit hit;

    private GameConfig _config;
    private InputSettings.InputData _settings;
    private PlayerCamera _camera;
    
    public PlayerInput(InputSettings.InputData settings, PlayerCamera camera, Transform pivot, Transform point, Transform forward)
    {
        _config = Configs.Get<GameConfig>();
        _settings = settings;
        _camera = camera;
        _pivot = pivot;
        _point = point;
        _forward = forward;
    }

    public void Execute()
    {
        DoMove();
        DoRotate();
        DoEvenets();
    }

    private void DoMove()
    {
        inputAxis.x = Input.GetAxis("Horizontal") * _settings.SpeedMove * Time.deltaTime;
        inputAxis.z = Input.GetAxis("Vertical") * _settings.SpeedMove * Time.deltaTime;

        position = Vector3.ClampMagnitude(position + _forward.TransformDirection(inputAxis), _settings.ClampAreaMovement);

        _point.localPosition = position;
    }
    
    private void DoRotate()
    {
        if (Input.GetMouseButton(1))
        {
            SetCurcor(CursorMode.Rotate);
                
            rotationAxis.x = Input.GetAxis("Mouse X") * _settings.RotateHorizontal * Time.deltaTime;
            rotationAxis.z = -Input.GetAxis("Mouse Y") * _settings.RotateVertical * Time.deltaTime;

            rotation.y += rotationAxis.x;
            rotation.x = Mathf.Clamp(rotation.x + rotationAxis.z, _settings.ClampMinVertical, _settings.ClampMaxVertical);

            forward.y = rotation.y;
                
            _point.localRotation = Quaternion.Euler(rotation);
            _forward.localRotation = Quaternion.Euler(forward);
        }
        else
        {
            SetCurcor(CursorMode.Free);
        }

        scrollValue += Input.mouseScrollDelta.y * _settings.SpeedScroll * Time.deltaTime;
        scrollValue = Mathf.Clamp(scrollValue, _settings.ScrollMinDistance, _settings.ScrollMaxDistance);
        _camera.SetHeight(-scrollValue);
    }

    private void DoEvenets()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _config.LayerGround))
                OnEventClick?.Invoke(hit.point);
        }
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