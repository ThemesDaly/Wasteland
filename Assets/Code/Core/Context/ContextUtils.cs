using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ContextUtils
{
    public static T SpawnManager<T>(in Dictionary<Type, BaseMonoController> list, BaseMonoController prefab) where T : BaseMonoController
    {
        Type type = typeof(T);
        BaseMonoController monoController = Object.Instantiate(prefab);
        
        monoController.Init();
        list.Add(type, monoController);

        return monoController as T;
    }

}