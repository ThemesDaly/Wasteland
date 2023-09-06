using UnityEngine;

public abstract class BaseMonoController : MonoBehaviour
{
    public abstract void Init();
    public abstract void DeInit();
    public abstract void Execute();
}