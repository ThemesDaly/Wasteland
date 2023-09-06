using UnityEngine;

public sealed class CockpitSystem : BaseSystem
{
    private CockpitView _view;
    private CockpitData _data;

    public override BaseView View() => _view;
    public override BaseObjectData Data() => _data;

    public override void Init(BaseView view, BaseObjectData data)
    {
        _view = view as CockpitView;
        _data = data as CockpitData;
    }

    public override void Execute()
    {
        
    }
}