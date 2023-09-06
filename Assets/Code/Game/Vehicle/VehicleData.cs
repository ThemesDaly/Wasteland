public class VehicleData : BaseObjectData
{
    public VehicleController Controller;

    public override void Init(string id, BaseController controller)
    {
        base.Init(id, controller);
        
        Controller = controller as VehicleController;
        Controller.Init(this);
    }
}