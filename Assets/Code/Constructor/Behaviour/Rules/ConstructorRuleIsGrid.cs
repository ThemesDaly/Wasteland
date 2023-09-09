public class ConstructorRuleIsGrid : ConstructorRule
{
    public ConstructorRuleIsGrid(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        var cells = Object.GetBounds();

        foreach (var cell in cells)
            if (_context.Grid[cell] == null)
                return false;
        
        return true;
    }
}