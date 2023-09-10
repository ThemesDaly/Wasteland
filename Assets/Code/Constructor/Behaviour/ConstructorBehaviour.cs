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

    public bool TryConstruction()
    {
        return false;
    }

    public bool TryObject(GridObject Object)
    {
        foreach (var rule in _rules)
            if(!rule.Execute(Object))
                return false;
        
        return true;
    }
}