using UnityEngine;

public interface IConstructorService
{
    void Load();
    void Save();
    void Exit();
}

public class ConstructorService: MonoBehaviour, IConstructorService
{
    public static IConstructorService Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }

    public void Save()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}