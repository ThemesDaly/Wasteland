using UnityEngine;

public interface IGameService
{
    GameContext Context { get; }

    void Load();
}

public class GameService : MonoBehaviour, IGameService
{
    public static IGameService Instance { get; private set; }

    public GameContext Context { get; private set; }

    private void Awake()
    {
        Instance = this;
        
        Context = new GameContext();
    }

    private void Update()
    {
        Context.Execute();
    }

    public void Load()
    {
        Context.Init();
        Context.CreateVehicle();


        var vehicleId = Context.Vehicles[0].Data().Id;
        var cockpit = Configs.Get<GameConfig>().Modules[0].Id;
        var platforma = Configs.Get<GameConfig>().Modules[1].Id;
        
        Context.CreateModule(vehicleId, cockpit);
        Context.CreateModule(vehicleId, platforma);
        Context.CreateModule(vehicleId, platforma);
        Context.CreateModule(vehicleId, platforma);
    }
}