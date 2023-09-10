using UnityEngine;

public static class ConstructorUtils
{
    private static Cell[] _cells;
    
    private static GridObjectLink[] _links;

    public static void StartMoveObjectToGrid(GridObject Object)
    {
        _cells = Object.GetBounds();
        _links = Object.GetLinks();
    }

    public static void PlaceObjectToGrid(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in _cells)
        {
            if (context.Grid[cell] != null)
                context.Grid[cell].RemoveObject(Object);
        }
        
        foreach (var cell in Object.GetBounds())
            if(context.Grid[cell] != null)
                context.Grid[cell].AddObject(Object);

        foreach (var link in _links)
        {
            foreach (var connector in link.Connectors)
            {
                if(context.Grid[connector.Cell] != null)
                {
                    context.Grid[connector.Cell].RemoveConnector(Object);
                    // Debug.Log($"Remove last connector cell {context.Grid[connector.Cell].Cell.Position}");
                }
            }
        }

        foreach (var link in Object.GetLinks())
        {
            foreach (var connector in link.Connectors)
            {
                if(context.Grid[connector.Cell] != null)
                {
                    context.Grid[connector.Cell].AddConnector(Object);
                    // Debug.Log($"Add connector cell {context.Grid[connector.Cell].Cell.Position}");
                }
            }
        }

        _cells = Object.GetBounds();
        _links = Object.GetLinks();
    }
}