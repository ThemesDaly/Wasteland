using UnityEngine;

public class PlayerConstructor : BaseMonoController
{
    private PlayerInput InputSystem; 
    
    private Transform _pivot;
    private Transform _point;
    private Transform _forward;

    public GridObject _target;

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

        _target = GameObject.FindFirstObjectByType<GridObject>();

        _pivot.position = new Vector3(Grid.GRID_SIZE_X, 0, Grid.GRID_SIZE_Z) / 2F;
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        InputSystem.Execute();
    }

    public void SetCenterGrid(Vector3 position) => _pivot.position = position;

    private void Enter(Vector3 position)
    {
        Services.Constructor.Context.MoveObject(_target, position.Vector3ToCell());
        // Debug.Log($"Enter: {position}");
    }
}