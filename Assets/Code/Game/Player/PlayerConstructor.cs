using System.Collections;
using UnityEngine;

public class PlayerConstructor : BaseMonoController
{
    private PlayerInput InputSystem; 
    
    private Transform _pivot;
    private Transform _point;
    private Transform _forward;

    private GridObject _target;
    
    private Vector3 _rayPoint;

    public override void Init()
    {
        _pivot = new GameObject("Pivot").transform;
        _pivot.SetParent(transform);
        
        _point = new GameObject("Point").transform;
        _point.SetParent(_pivot);
        
        _forward = new GameObject("Forward").transform;
        _forward.SetParent(transform);
        
        InputSystem = new PlayerInput(Configs.Get<InputSettings>().Constructor, 
                                      Services.Constructor.Context.PlayerCamera,
                                      Configs.Get<GameConfig>().LayerGrid,
                                      _pivot,
                                      _point, 
                                      _forward);

        Services.Constructor.Context.PlayerCamera.SetFollow(_point);

        _pivot.position = new Vector3(Grid.GRID_SIZE_X, 0, Grid.GRID_SIZE_Z) / 2F;
        
        InputSystem.OnUpdate += Enter;
        ServicesEvents.Constructor.OnDrag += Selected;
        ServicesEvents.Constructor.OnDrop += Drop;
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();

        if (_target)
        {
            if(Input.GetMouseButtonUp(0))
                Services.Constructor.Context.Drop(_target);
            else
                Services.Constructor.Context.MoveObject(_target, _rayPoint);
        }
    }

    private void Selected(GridObject Object)
    {
        _target = Object;
    }

    private void Drop(GridObject Object)
    {
        _target = null;
    }

    private void Enter(Vector3 position)
    {
        _rayPoint = position;
        ServicesEvents.Constructor.CursorCell(position);
    }

    public void SetCenter(Vector3 position) => _pivot.position = position;
}