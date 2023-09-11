using System.Collections;
using UnityEngine;

public class PlayerConstructor : BaseMonoController
{
    public Vector3 Position => InputSystem.Point.position;

    private PlayerInput InputSystem;
    
    private GridObject _target;

    public override void Init()
    {
        InputSystem = new PlayerInput(Configs.Get<InputSettings>().Constructor, 
                                      Services.Constructor.Context.PlayerCamera,
                                      transform);

        Services.Constructor.Context.PlayerCamera.SetFollow(InputSystem.Point);

        InputSystem.Pivot.position = new Vector3(Grid.GRID_SIZE_X, 0, Grid.GRID_SIZE_Z) / 2F;
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();
    }
    
    public void SetCenter(Vector3 position) => InputSystem.Pivot.position = position;

    public void SetFloor(int height) => InputSystem.Pivot.position = InputSystem.Pivot.position.WithY(height);
}