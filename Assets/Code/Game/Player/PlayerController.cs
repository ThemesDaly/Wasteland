using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : BaseManager
{
    private IVehicle _vehicle;

    private PlayerInput InputSystem; 
    
    private Transform _pivot;
    private Transform _point;
    private Transform _forward;

    public override void Init()
    {
        _pivot = new GameObject("Pivot").transform;
        _pivot.SetParent(transform);
        
        _point = new GameObject("Point").transform;
        _point.SetParent(_pivot);
        
        _forward = new GameObject("Forward").transform;
        _forward.SetParent(transform);
        
        InputSystem = new PlayerInput(_pivot,_point, _forward);
        InputSystem.OnEventClick += MoveToPoint;
        
        Services.Game.Context.Camera.SetFollow(_point);
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();

        _pivot.position = _vehicle.Position;
    }

    private void MoveToPoint(Vector3 worldPosition)
    {
        _vehicle.MoveToPoint(worldPosition);   
    }

    public void AddVehicle(IVehicle vehicle)
    {
        _vehicle = vehicle;
    }
}