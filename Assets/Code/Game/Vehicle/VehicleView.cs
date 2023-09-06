using UnityEngine;

public class VehicleView : BaseView
{
    [SerializeField] private Transform _content;
    
    private VehicleData _object;

    public Transform Content => _content;
    
    public override void Init(BaseObjectData data)
    {
        base.Init(data);
        
        _object = data as VehicleData;
    }
}