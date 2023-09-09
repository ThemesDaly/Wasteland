public class ConstructorRuleIsGrid : ConstructorRule
{
    public ConstructorRuleIsGrid(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        foreach (var cell in Object.GetBounds())
            if (_context.Grid[cell] == null)
                return false;
        
        return true;
    }
}