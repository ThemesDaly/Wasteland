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

    public List<GridObject> Objects { get; private set; }

    private bool _isInit;

    public void Init()
    {
        Grid = new Grid(64, 64);
        Grid.Init();
        
        GridPlane = ContextUtils.SpawnManager<GridPlane>(_controllers, Configs.Get<GameConfig>().gridPlane);

        Behaviour = new ConstructorBehaviour(this);
        Tools = new ConstructorTools(this, Behaviour);
        Manager = new ConstructorManager(this, Tools);
        Objects = new List<GridObject>();

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
        prefabContainer.transform.position = PlayerGame.Position.ToCell();
        prefabContainer.Unpack();
        
        objectContainer.MoveTo(PlayerGame.Position.ToCell());
        ConstructorUtils.PlaceObject(objectContainer);
        
        Objects.Add(objectContainer);
        Behaviour.TryObjects(Objects);
    }

    public void RemoveModule(GridObject Object)
    {
        ConstructorUtils.ClearObject(Object);
        GameObject.Destroy(Object.Data.Target.gameObject);
        GameObject.Destroy(Object.gameObject);
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