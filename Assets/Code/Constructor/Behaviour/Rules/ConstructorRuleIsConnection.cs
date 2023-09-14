using UnityEngine;

public class ConstructorRuleIsConnection : ConstructorRule
{
    public ConstructorRuleIsConnection(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        var connectors = Object.GetConnectors();

        if (connectors.Length == 0)
            return true;

        int requiredCount = 0;
        int requiredConnection = 0;
        int allConnection = 0;
        
        foreach (var connector in connectors)
        {
            if (connector.Data.Required == GridObjectConnector.ConnectorRequired.Required)
                requiredCount++;
            
            if (_context.Grid[connector.Cell] != null && _context.Grid[connector.Cell].Connectors.Count == 2)
            {
                GridObject other = null;
                
                foreach (var c in _context.Grid[connector.Cell].Connectors)
                    if (!c.Equals(Object))
                        other = c;

                if (ConstructorUtils.TryConnectionObjects(Object, other))
                {
                    if (connector.Data.Required == GridObjectConnector.ConnectorRequired.Required)
                        requiredConnection++;
                    else if (connector.Data.Required == GridObjectConnector.ConnectorRequired.DontRequired)
                        allConnection++;
                }
            }
        }

        if (requiredCount != 0)
        {
            return requiredConnection.Equals(requiredCount);
        }
        else
        {
            return allConnection > 0;   
        }
    }
}