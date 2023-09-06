using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameContext
{
    public CameraController Camera { get; private set; }
    
    public PlayerController Player { get; private set; }

    private static readonly Dictionary<Type, BaseMonoController> _dataModules = new Dictionary<Type, BaseMonoController>();

    public void Init()
    {
        // SpawnCamera();
        // SpawnPlayer();
    }

    public void Execute()
    {
        
    }

    private void SpawnCamera()
    {
        Camera = SpawnManager<CameraController>(Configs.Get<GameConfig>().CameraController);
    }

    private void SpawnPlayer()
    {
        Player = SpawnManager<PlayerController>(Configs.Get<GameConfig>().PlayerController);
    }
    
    private static T SpawnManager<T>(BaseMonoController prefabMonoController) where T : BaseMonoController
    {
        Type type = typeof(T);
        BaseMonoController monoController = Object.Instantiate(prefabMonoController);
        
        monoController.Init();
        _dataModules.Add(type, monoController);

        return monoController as T;
    }
}