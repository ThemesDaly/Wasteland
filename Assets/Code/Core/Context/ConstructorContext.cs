using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ConstructorContext : ICoreSystem
{
    public PlayerCamera PlayerCamera { get; private set; }
    
    public PlayerConstructor PlayerGame { get; private set; }

    public GridPlane GridPlane { get; private set; }
    
    public Grid Grid { get; private set; }

    private ConstructorBehaviour Behaviour;

    private ConstructorManager Manager; 

    private ConstructorTools Tools;
    
    private static readonly Dictionary<Type, BaseMonoController> _controllers = new Dictionary<Type, BaseMonoController>();

    // private GridObject _targetObject;
    //
    // private Vector3 _offsetPosition;
    //
    // private Vector3 _updatePosition;

    private bool _isInit;

    public void Init()
    {
        Grid = new Grid(64, 64);
        Grid.Init();
        
        GridPlane = ContextUtils.SpawnManager<GridPlane>(_controllers, Configs.Get<GameConfig>().gridPlane);

        Behaviour = new ConstructorBehaviour(this);
        Tools = new ConstructorTools(this, Behaviour);
        Manager = new ConstructorManager(this, Tools);

        _isInit = true;
    }

    public void DeInit()
    {
        Behaviour = null;
        Manager = null;
        Tools = null;

        _isInit = false;
    }

    public void Execute()
    {
        if(!_isInit)
            return;
        
        foreach (var controller in _controllers)
            controller.Value.Execute();

        Manager.Execute();
    }

    public void AddModule(string inventoryId)
    {
        var moduleContainer = Configs.Get<GameConfig>().Modules.GetById(inventoryId).BaseData.Container;
        var prefabContainer = Object.Instantiate(moduleContainer);
        var objectContainer = prefabContainer.Object;
        prefabContainer.Init();
        
        objectContainer.MoveTo(PlayerGame.Position.ToCell());
        ConstructorUtils.PlaceObject(objectContainer);
        Behaviour.TryObjects(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
    }

    public void RemoveModule(GridObject Object)
    {
        
    }
    
    // public void Drag(GridObject Object)
    // {
    //     if(_targetObject != null)
    //         return;
    //     
    //     _targetObject = Object;
    //     _updatePosition = -Vector3.one;
    //     _offsetPosition = -Vector3.one;
    //
    //     ServicesEvents.Constructor.Drag(Object);
    //     ConstructorUtils.StartMoveObject(Object);
    // }
    //
    // public void Drop(GridObject Object)
    // {
    //     if(_targetObject == null)
    //         return;
    //     
    //     _targetObject = null;
    //     
    //     ServicesEvents.Constructor.Drop(Object);
    //     Behaviour.TryObjects(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
    // }

    // public void PointIn(GridObject Object)
    // {
    //     if(_targetObject != null)
    //         return;
    //     
    //     ServicesEvents.Constructor.PointIn(Object);
    // }
    //
    // public void PointOut(GridObject Object)
    // {
    //     if(_targetObject != null)
    //         return;
    //     
    //     ServicesEvents.Constructor.PointOut(Object);
    // }

    // public void MoveObject(GridObject Object, Vector3 position)
    // {
    //     if (_offsetPosition.Equals(-Vector3.one))
    //         _offsetPosition = Object.transform.position - position;
    //     
    //     if(position.ToCell() == _updatePosition)
    //         return;
    //
    //     Object.MoveTo((position + _offsetPosition).ToCell());
    //     ConstructorUtils.MoveObject(Object);
    //     Behaviour.TryObjects(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
    //     
    //     _updatePosition = position.ToCell();
    // }
    
    public void SpawnPlayer()
    {
        if(PlayerGame != null)
            GameObject.Destroy(PlayerGame.gameObject);
        
        if(PlayerCamera != null)
            GameObject.Destroy(PlayerCamera.gameObject);
        
        PlayerCamera = ContextUtils.SpawnManager<PlayerCamera>(_controllers, Configs.Get<GameConfig>().PlayerCamera);
        PlayerGame = ContextUtils.SpawnManager<PlayerConstructor>(_controllers, Configs.Get<GameConfig>().PlayerConstructorController);
    }
}