using UnityEngine;

public sealed class PlatformaSystem : BaseSystem
{
    private PlatformaView _view;
    private PlatformaData _data;

    public override BaseView View() => _view;
    public override BaseObjectData Data() => _data;

    public override void Init(BaseView view, BaseObjectData data)
    {
        _view = view as PlatformaView;
        _data = data as PlatformaData;
    }

    public override void Execute()
    {
        
    }
}