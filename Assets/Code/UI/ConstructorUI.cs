using UnityEngine;

public class ConstructorUI : BaseCanvasUI
{
    [SerializeField] private TextPropertyUI _propetyCellObject;

    public override void Init(Canvas canvas)
    {
        base.Init(canvas);

        _propetyCellObject.DisplayTitle("Object");
        ServicesEvents.Constructor.OnDrag += (GridObject g) => { _propetyCellObject.DisplayValue("Drag"); };
        ServicesEvents.Constructor.OnPointIn += (GridObject g) => { _propetyCellObject.DisplayValue("Enter"); };
        ServicesEvents.Constructor.OnPointOut += (GridObject g) => { _propetyCellObject.DisplayValue("null"); };
    }
}