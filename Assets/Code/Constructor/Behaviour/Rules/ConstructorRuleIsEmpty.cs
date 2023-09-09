public class ConstructorRuleIsEmpty : ConstructorRule
{
    public ConstructorRuleIsEmpty(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        var cells = Object.GetBounds();

        foreach (var cell in cells)
        {
            if(_context.Grid[cell].Contains(Object))
                continue;
            
            if (!_context.Grid[cell].IsEmpty)
                return false;
        }

        return true;
    }
}