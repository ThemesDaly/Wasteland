using UnityEngine;

public class ConstructorUI : BaseCanvasUI
{
    [SerializeField] private TextPropertyUI _propetyCellObject;
    [SerializeField] private TextPropertyUI _propetyCoordinate;

    public override void Init(Canvas canvas)
    {
        base.Init(canvas);

        _propetyCellObject.DisplayTitle("Object");
        ServicesEvents.Constructor.OnDrag += (GridObject g) => { _propetyCellObject.DisplayValue("Drag"); };
        ServicesEvents.Constructor.OnPointIn += (GridObject g) => { _propetyCellObject.DisplayValue("Enter"); };
        ServicesEvents.Constructor.OnPointOut += (GridObject g) => { _propetyCellObject.DisplayValue("null"); };
        
        _propetyCoordinate.DisplayTitle("Coordinate");
        ServicesEvents.Constructor.OnCursorCell += (Vector3 v) => { _propetyCoordinate.DisplayValue(v.ToString()); };
    }
}