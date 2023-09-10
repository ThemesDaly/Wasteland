using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorContext : ICoreSystem
{
    public PlayerCamera PlayerCamera { get; private set; }
    
    public PlayerConstructor PlayerGame { get; private set; }

    public GridPlane GridPlane { get; private set; }
    
    public Grid Grid { get; private set; }

    private ConstructorBehaviour Behaviour;
    
    private static readonly Dictionary<Type, BaseMonoController> _controllers = new Dictionary<Type, BaseMonoController>();

    private GridObject _targetObject;

    private Vector3 _lastPosition;

    public void Init()
    {
        Grid = new Grid(64, 64);
        Grid.Init();
        
        GridPlane = ContextUtils.SpawnManager<GridPlane>(_controllers, Configs.Get<GameConfig>().gridPlane);

        Behaviour = new ConstructorBehaviour(this);
    }

    public void DeInit()
    {
        
    }

    public void Execute()
    {
        foreach (var controller in _controllers)
            controller.Value.Execute();
    }
    
    public void Drag(GridObject Object)
    {
        if(_targetObject != null)
            return;
        
        _targetObject = Object;
        _lastPosition = -Vector3.one;

        ServicesEvents.Constructor.Drag(Object);
        ConstructorUtils.StartMoveObjectToGrid(Object);
    }

    public void Drop(GridObject Object)
    {
        if(_targetObject == null)
            return;
        
        _targetObject = null;
        
        ServicesEvents.Constructor.Drop(Object);
        Behaviour.TryConstruction(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
    }

    public void PointIn(GridObject Object)
    {
        if(_targetObject != null)
            return;
        
        ServicesEvents.Constructor.PointIn(Object);
    }
    
    public void PointOut(GridObject Object)
    {
        if(_targetObject != null)
            return;
        
        ServicesEvents.Constructor.PointOut(Object);
    }

    public void MoveObject(GridObject Object, Vector3 position)
    {
        if(position.ToCell() == _lastPosition)
            return;

        Object.MoveTo(position.ToCell());
        ConstructorUtils.PlaceObjectToGrid(Object);
        // Behaviour.TryObject(Object);
        Behaviour.TryConstruction(GameObject.FindObjectsByType<GridObject>(FindObjectsSortMode.InstanceID));
        
        _lastPosition = position.ToCell();
    }
    
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