public class Module : BaseDatabaseItem, IStringIdItem
{
    public ModuleBaseData BaseData;
    
    public override void DatabaseInit(string id, string name)
    {
        
    }

    public string ItemId => GetDatabaseId();
    public string ItemName => GetDatabaseName();

    public override string GetDatabaseId() => BaseData.Id;
    public override string GetDatabaseName() => BaseData.Name;
}