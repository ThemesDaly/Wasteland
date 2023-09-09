using System;
using UnityEngine;

[Serializable]
public class GridObjectData
{
    public Transform target;

    public Result result;
    
    public enum Result : int
    {
        Failed = 0,
        Unlock = 1,
        Successfully = 2
    }
}