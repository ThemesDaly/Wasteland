public class ConstructorRuleIsEmpty : ConstructorRule
{
    public ConstructorRuleIsEmpty(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        foreach (var cell in Object.GetBounds())
        {
            foreach (var other in _context.Grid[cell].Objects)
                if (!other.Equals(Object))
                    return false;
        }

        return true;
    }
}