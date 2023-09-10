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
                if (_context.Grid[connector.Cell].LinkObjects.Count == 2)
                {
                    var firstObject = _context.Grid[connector.Cell].LinkObjects[0];
                    var secondObject = _context.Grid[connector.Cell].LinkObjects[1];
                    
                    return ConstructorUtils.TryConnectionObjects(firstObject, secondObject);
                }
                
            }
        }

        return false;
    }
}