using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameContext
{
    public CameraController Camera { get; private set; }
    
    public PlayerController Player { get; private set; }

    private static readonly Dictionary<Type, BaseManager> _dataModules = new Dictionary<Type, BaseManager>();

    public List<VehicleSystem> Vehicles { get; private set; }

    public void Init()
    {
        Vehicles = new List<VehicleSystem>();
        
        SpawnCamera();
        SpawnPlayer();
    }

    public void Execute()
    {
        // foreach (var module in _dataModules)
        //     module.Value.Execute();
        //
        // foreach (var vehicle in Vehicles)
        //     vehicle.Execute();
    }

    public void CreateVehicle()
    {
        var data = new VehicleData();
        var system = new VehicleSystem();
        var id = Guid.NewGuid().ToString("N");

        var parent = new GameObject("Vehicle [Container]").transform;
        var view = Object.Instantiate(Configs.Get<GameConfig>().VehicleView, parent);
        var controller = Object.Instantiate(Configs.Get<GameConfig>().VehicleController, parent);


        data.Init(id, controller);
        view.Init(data);
        system.Init(view, data);
        
        Player.AddVehicle(system);
        Vehicles.Add(system);
        
    }

    public void CreateModule(string vehicleId, string moduleId)
    {
        if (GetVehicle(vehicleId, out VehicleSystem vehicle) &&
            Configs.Get<GameConfig>().GetModule(moduleId, out BaseModule module))
        {
            module.Init(out BaseSystem system, out BaseObjectData data);
            vehicle.AddModule(system, data.Controller);
        }
    }

    public bool GetVehicle(string id, out VehicleSystem vehicleSystem)
    {
        vehicleSystem = null;
        
        foreach (var vehicle in Vehicles)
        {
            if (vehicle.Data().Id == id)
            {
                vehicleSystem = vehicle;
                break;
            }
        }

        return vehicleSystem != null;
    }

    private void SpawnCamera()
    {
        Camera = SpawnManager<CameraController>(Configs.Get<GameConfig>().CameraController);
    }

    private void SpawnPlayer()
    {
        Player = SpawnManager<PlayerController>(Configs.Get<GameConfig>().PlayerController);
    }
    
    private static T SpawnManager<T>(BaseManager prefabManager) where T : BaseManager
    {
        Type type = typeof(T);
        BaseManager manager = Object.Instantiate(prefabManager);
        
        manager.Init();
        _dataModules.Add(type, manager);

        return manager as T;
    }
}