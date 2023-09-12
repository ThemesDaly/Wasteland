using UnityEngine;

public class ConstructorRuleIsLink : ConstructorRule
{
    public ConstructorRuleIsLink(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        var connectors = Object.GetConnectors();

        if (connectors.Length == 0)
            return true;
        
        foreach (var connector in connectors)
        {
            if (_context.Grid[connector.Cell].Connectors.Count == 2)
            {
                var firstObject = _context.Grid[connector.Cell].Connectors[0];
                var secondObject = _context.Grid[connector.Cell].Connectors[1];

                if (ConstructorUtils.TryConnection(firstObject, secondObject))
                    return true;
            }
        }

        return false;
    }
}