using UnityEngine;

public class ConstructorRuleIsLink : ConstructorRule
{
    public ConstructorRuleIsLink(ConstructorContext context) : base(context) { }

    public override bool Execute(GridObject Object)
    {
        var links = Object.GetLinks();

        if (links.Length == 0)
            return true;
        
        foreach (var link in links)
        {
            foreach (var connector in link.Connectors)
            {
                Debug.Log($"Link count: {_context.Grid[connector.Cell].LinkObjects.Count}");
                
                if (_context.Grid[connector.Cell].LinkObjects.Count == 2)
                    return true;
                
            }
        }

        return false;
    }
}