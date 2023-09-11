using UnityEngine;

public class PlayerGame : BaseMonoController
{
    private PlayerInput InputSystem;

    public override void Init()
    {
        InputSystem = new PlayerInput(Configs.Get<InputSettings>().Game, 
                                      Services.Game.Context.PlayerCamera,
                                      transform);

        Services.Game.Context.PlayerCamera.SetFollow(InputSystem.Point);
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();
    }
}