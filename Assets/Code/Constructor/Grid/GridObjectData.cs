using System;
using UnityEngine;

[Serializable]
public class GridObjectData
{
    public Result result;
    public Transform Target { get; private set; }

    public void SetTarget(Transform target) => Target = target;
    
    public enum Result : int
    {
        Failed = 0,
        Unlock = 1,
        Successfully = 2
    }
}