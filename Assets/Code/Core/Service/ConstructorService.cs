using UnityEngine;

public interface IConstructorService
{
    ConstructorContext Context { get; }
    
    void Load();
    void Save();
    void Exit();
}

public class ConstructorService: MonoBehaviour, IConstructorService
{
    public static IConstructorService Instance { get; private set; }
    
    public ConstructorContext Context { get; private set; }

    private void Awake()
    {
        Instance = this;

        Context = new ConstructorContext();
    }

    private void Update()
    {
        Context.Execute();
    }

    public void Load()
    {
        Context.Init();
        Context.SpawnPlayer();
    }

    public void Save()
    {
        
    }

    public void Exit()
    {
        
    }
}