public sealed class ConstructorBehaviour
{
    private ConstructorRule[] _rules;

    public ConstructorBehaviour(ConstructorContext context)
    {
        _rules = new ConstructorRule[]
        {
            new ConstructorRuleIsGrid(context),
            new ConstructorRuleIsEmpty(context),
            new ConstructorRuleIsLink(context)
        };
    }

    public bool TryObjects(GridObject[] Objects)
    {
        bool result = true;
        
        foreach (var Object in Objects)
        {
            if (!TryObject(Object))
                result = false;
        }
        
        return result;
    }

    public bool TryObject(GridObject Object)
    {
        foreach (var rule in _rules)
        {
            if (!rule.Execute(Object))
            {
                Object.Data.result = GridObjectData.Result.Failed;
                return false;
            }
        }
        
        Object.Data.result = GridObjectData.Result.Unlock;
        return true;
    }
}