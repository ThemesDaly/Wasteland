using System;
using NaughtyAttributes;
using UnityEngine;

public abstract class BaseModule : ScriptableObject
{
    [BoxGroup("Data")] public string Id;
    [BoxGroup("Data")] public BaseView View;
    [BoxGroup("Data")] public PhysicsModule physicsModule;

    public BaseModule()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public abstract void Init(out BaseSystem system, out BaseObjectData data);
}