using UnityEngine;

public class PlayerConstructor : BaseMonoController
{
    private PlayerInput InputSystem; 
    
    private Transform _pivot;
    private Transform _point;
    private Transform _forward;

    public ConstructorMonoObject _target;

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

        InputSystem.OnUpdate += Enter;

        _target = GameObject.FindFirstObjectByType<ConstructorMonoObject>();
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();
    }

    private void Enter(Vector3 position)
    {
        _target.transform.position = position.NormalizeD();
        Debug.Log($"Enter: {position}");
    }
}