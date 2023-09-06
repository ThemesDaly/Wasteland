using System.Collections.Generic;
using UnityEngine;

public interface IVehicle
{
    Vector3 Position { get; }
    void MoveToPoint(Vector3 worldPosition);
}

public class VehicleSystem : BaseSystem, IVehicle
{
    private VehicleView _view;
    private VehicleData _data;

    public override BaseView View() => _view;
    public override BaseObjectData Data() => _data;

    public Vector3 Position { get => _data.Position; }

    private List<BaseSystem> _modules;

    public override void Init(BaseView view, BaseObjectData data)
    {
        _view = view as VehicleView;
        _data = data as VehicleData;
        _modules = new List<BaseSystem>();
    }

    public override void Execute()
    {
        foreach (var module in _modules)
            module.Execute();
    }

    public void AddModule(BaseSystem module, BaseController controller)
    {
        module.View().transform.SetParent(_view.Content);
        controller.transform.SetParent(_data.Controller.transform);
        
        var connector = controller as PhysicsModule;
        var view = module.View() as ViewModule;
        
        connector.Setup(view.Collider);
        
        if(_modules.Count > 0)
        {
            var lastModule = _modules[_modules.Count - 1];
            var lastConnector = lastModule.Data().Controller as PhysicsModule;
            
            connector.Connect(lastConnector);
        }

        _modules.Add(module);
    }

    public void MoveToPoint(Vector3 worldPosition)
    {
        _data.Controller.MoveToPoint(worldPosition);
    }
}