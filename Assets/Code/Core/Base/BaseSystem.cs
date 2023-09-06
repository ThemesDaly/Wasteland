public abstract class BaseSystem
{
    public abstract BaseView View();
    public abstract BaseObjectData Data();
    public abstract void Init(BaseView view, BaseObjectData data);
    public abstract void Execute();
    
}