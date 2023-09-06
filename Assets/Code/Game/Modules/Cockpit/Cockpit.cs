using System;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = AssetDefineConstants.VEHICLE + AssetDefineConstants.VEHICLE_MODULES + "Cockpit", fileName = "Cockpit")]
public sealed class Cockpit : BaseModule
{
    public override void Init(out BaseSystem system, out BaseObjectData data)
    {
        system = new CockpitSystem();
        data = new CockpitData();
        var id = Guid.NewGuid().ToString("N");
            
        var view = Object.Instantiate(View);
        var controller = Object.Instantiate(physicsModule);

        data.Init(id, controller);
        view.Init(data);
        system.Init(view, data);
    }
}