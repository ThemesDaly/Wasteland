using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorContext : ICoreSystem
{
    public PlayerCamera PlayerCamera { get; private set; }
    
    public PlayerConstructor PlayerGame { get; private set; }

    public GridPlane GridPlane { get; private set; }
    
    public Grid Grid { get; private set; }
    
    private static readonly Dictionary<Type, BaseMonoController> _controllers = new Dictionary<Type, BaseMonoController>();

    public void Init()
    {
        Grid = new Grid(64, 64);
        Grid.Init();
        
        GridPlane = ContextUtils.SpawnManager<GridPlane>(_controllers, Configs.Get<GameConfig>().gridPlane);
    }

    public void DeInit()
    {
        
    }

    public void Execute()
    {
        foreach (var controller in _controllers)
            controller.Value.Execute();
    }

    public void MoveObject(GridObject Object, Vector3 position)
    {
        Object.MoveTo(position);

        // Cell pivot = Cell.Create(position);
        // Grid[pivot].IsBusy = true;
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