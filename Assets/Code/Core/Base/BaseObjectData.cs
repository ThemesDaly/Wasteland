using System;
using UnityEngine;

public abstract class BaseObjectData
{
    public Action<PropertyFieldData, object> OnChanged;

    public virtual void Init(string id, BaseController controller)
    {
        Id = id;
        Controller = controller;
        Controller.Init(this);
    }
    
    public string Id { get; private set; }

    public BaseController Controller;

    private Vector3 _position;
    public Vector3 Position
    {
        get => _position;
        set
        {
            if (!_position.Equals(value))
            {
                _position = value;
                OnChanged?.Invoke(PropertyFieldData.Position, value);
            }
        }
    }
    
    private Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set
        {
            if (!_direction.Equals(value))
            {
                _direction = value;
                OnChanged?.Invoke(PropertyFieldData.Direction, value);
            }
        }
    }
    
    public enum PropertyFieldData
    {
        Position,
        Direction
    }
}