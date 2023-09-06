using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    public abstract void Init();
    public abstract void DeInit();
    public abstract void Execute();
}