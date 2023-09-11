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
    }

    public void Drop(GridObject Object)
    {
        if(_targetObject == null)
            return;
        
        _targetObject = null;
        
        ServicesEvents.Constructor.Drop(Object);
        _behaviour.TryObjects(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
    }
    
    public void MoveObject(GridObject Object, Vector3 position)
    {
        if (_offsetPosition.Equals(-Vector3.one))
            _offsetPosition = Object.transform.position - position;
        
        if(position.ToCell() == _updatePosition)
            return;

        Object.MoveTo((position + _offsetPosition).ToCell());
        ConstructorUtils.MoveObject(Object);
        _behaviour.TryObjects(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
        
        _updatePosition = position.ToCell();
    }
    
    private void FloorUp()
    {
        if(_floorHeight + 1 <= Grid.GRID_SIZE_Y)
        {
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