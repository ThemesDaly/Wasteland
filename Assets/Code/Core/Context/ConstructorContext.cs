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

    private Cell[] _cells;

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
        _cells = Object.GetBounds();
        
        ServicesEvents.Constructor.Drag(Object);
    }

    public void Drop(GridObject Object)
    {
        if(_targetObject == null)
            return;

        foreach (var cell in _cells)
            Grid[cell].Remove(Object);
        
        foreach (var cell in Object.GetBounds())
            Grid[cell].Add(Object);
        
        _targetObject = null;
        _cells = null;
        
        ServicesEvents.Constructor.Drop(Object);
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
        bool isAllowed = Behaviour.TryObject(Object);

        Object.MoveTo(position.ToCell());
        
        Debug.Log($"IsAllowed: {isAllowed}");
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