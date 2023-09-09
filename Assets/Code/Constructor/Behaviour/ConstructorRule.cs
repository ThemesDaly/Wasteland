public abstract class ConstructorRule
{
    protected ConstructorContext _context;

    public ConstructorRule(ConstructorContext context)
    {
        this._context = context;
    }

    public abstract bool Execute(GridObject Object);
}