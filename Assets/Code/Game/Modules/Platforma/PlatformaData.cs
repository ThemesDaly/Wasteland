public sealed class PlatformaData : BaseObjectData
{
    public PhysicsModule PhysicsModule;
    
    public override void Init(string id, BaseController controller)
    {
        base.Init(id, controller);
        
        PhysicsModule = controller as PhysicsModule;
        PhysicsModule.Init(this);
    }
}