using UnityEngine;

public class ConstructorUI : BaseCanvasUI
{
    [SerializeField] private TextPropertyUI _propetyCellObject;

    public override void Init(Canvas canvas)
    {
        base.Init(canvas);

        _propetyCellObject.DisplayTitle("Cell Object");
        ServicesEvents.Constructor.OnDrag += (GridObject g) => { _propetyCellObject.DisplayValue("Selected"); };
        ServicesEvents.Constructor.OnPointIn += (GridObject g) => { _propetyCellObject.DisplayValue("Point In"); };
        ServicesEvents.Constructor.OnPointOut += (GridObject g) => { _propetyCellObject.DisplayValue("Free"); };
    }
}