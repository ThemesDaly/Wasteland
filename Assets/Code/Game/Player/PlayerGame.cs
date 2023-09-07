using UnityEngine;

public class PlayerGame : BaseMonoController
{
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
        
        InputSystem = new PlayerInput(Configs.Get<InputSettings>().Game, 
                                      Services.Game.Context.PlayerCamera, 
                                      _pivot,
                                      _point, 
                                      _forward);

        Services.Game.Context.PlayerCamera.SetFollow(_point);
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();
    }
}