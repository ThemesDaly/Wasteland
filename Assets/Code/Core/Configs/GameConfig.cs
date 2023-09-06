using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = AssetDefineConstants.CONFIGS + "GameConfig", fileName = "GameConfig")]
public class GameConfig : Config
{
    [BoxGroup("Prefabs")] public CameraController CameraController;
    [BoxGroup("Prefabs")] public PlayerController PlayerController;
    [BoxGroup("Prefabs")] public VehicleController VehicleController;
    [BoxGroup("Prefabs")] public VehicleView VehicleView;

    [BoxGroup("World")] public LayerMask LayerGround;

    private BaseModule[] _modules;
    public BaseModule[] Modules => _modules;

    public override void Init()
    {
        _modules = Resources.LoadAll<BaseModule>(AssetDefineConstants.VEHICLE_MODULES);
    }

    public bool GetModule(string id, out BaseModule baseModule)
    {
        baseModule = null;

        foreach (var p in _modules)
        {
            if (p.Id == id)
            {
                baseModule = p;
                break;
            }
        }

        return baseModule != null;
    }
}