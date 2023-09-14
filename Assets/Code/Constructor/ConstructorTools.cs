using System.Collections.Generic;
using UnityEngine;

public sealed class ConstructorTools
{
    private GridObject _targetObject;

    private Vector3 _offsetPosition;
    
    private Vector3 _updatePosition;
    
    private int _floorHeight;
    
    private ConstructorContext _context;

    private ConstructorBehaviour _behaviour;
    
    public ConstructorTools(ConstructorContext context, ConstructorBehaviour behaviour)
    {
        _context = context;
        _behaviour = behaviour;
        
        ServicesEvents.Constructor.OnFloorUp += FloorUp;
        ServicesEvents.Constructor.OnFloorDown += FloorDown;
    }

    ~ConstructorTools()
    {
        ServicesEvents.Constructor.OnFloorUp -= FloorUp;
        ServicesEvents.Constructor.OnFloorDown -= FloorDown;
    }
    
    public void Drag(GridObject Object)
    {
        if(_targetObject != null)
            return;
        
        _targetObject = Object;
        _updatePosition = -Vector3.one;
        _offsetPosition = -Vector3.one;

        ServicesEvents.Constructor.Drag(Object);
        ConstructorUtils.StartMoveObject(Object);

        foreach (var connector in Object.GetConnectors())
            if (connector.Data.Required == GridObjectConnector.ConnectorRequired.Required)
                ServicesEvents.Constructor.ShowConnectors(Object, connector.Data);
    }

    public void Drop(GridObject Object)
    {
        if(_targetObject == null)
            return;
        
        _targetObject = null;
        
        ServicesEvents.Constructor.Drop(Object);
        _behaviour.TryObjects(_context.Objects);
        
        foreach (var connector in Object.GetConnectors())
            if (connector.Data.Required == GridObjectConnector.ConnectorRequired.Required)
                ServicesEvents.Constructor.HideConnectors(Object, connector.Data);
    }
    
    public void MoveObject(GridObject Object, Vector3 position)
    {
        if (_offsetPosition.Equals(-Vector3.one))
            _offsetPosition = (Object.transform.position - position).WithY(0);
        
        if(position.ToCell() == _updatePosition)
            return;

        Object.MoveTo(position + _offsetPosition);
        ConstructorUtils.ReplaceObject(Object);
        _behaviour.TryObjects(_context.Objects);
        
        _updatePosition = position.ToCell();
    }
    
    private void FloorUp()
    {
        if(_floorHeight + 1 < Grid.GRID_SIZE_Y)
        {
            Debug.Log($"Max height: {Grid.GRID_SIZE_Y} current {_floorHeight + 1}");
            _context.GridPlane.SetFloor(++_floorHeight);
            _context.PlayerGame.SetFloor(_floorHeight);
        }
    }

    private void FloorDown()
    {
        if(_floorHeight - 1 >= 0)
        {
            _context.GridPlane.SetFloor(--_floorHeight);
            _context.PlayerGame.SetFloor(_floorHeight);
        }
    }
}