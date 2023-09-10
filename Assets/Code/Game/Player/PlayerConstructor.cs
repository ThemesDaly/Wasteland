using System.Collections;
using UnityEngine;

public class PlayerConstructor : BaseMonoController
{
    public Vector3 Position => InputSystem.Pivot.position;
    
    private PlayerInput InputSystem;
    
    private GridObject _target;

    public override void Init()
    {
        InputSystem = new PlayerInput(Configs.Get<InputSettings>().Constructor, 
                                      Services.Constructor.Context.PlayerCamera,
                                      Configs.Get<GameConfig>().LayerGrid,
                                      transform);

        Services.Constructor.Context.PlayerCamera.SetFollow(InputSystem.Point);

        InputSystem.Pivot.position = new Vector3(Grid.GRID_SIZE_X, 0, Grid.GRID_SIZE_Z) / 2F;
        
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
                Services.Constructor.Context.MoveObject(_target, InputSystem.InputPosition);
        }
        
        ServicesEvents.Constructor.CursorCell(InputSystem.InputPosition);
    }

    private void Selected(GridObject Object)
    {
        _target = Object;
    }

    private void Drop(GridObject Object)
    {
        _target = null;
    }
    
    public void SetCenter(Vector3 position) => InputSystem.Pivot.position = position;
}