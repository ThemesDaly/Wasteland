using UnityEngine;

public static class ConstructorUtils
{
    private static Cell[] _cells;
    
    private static GridObjectLink[] _links;

    public static void StartMoveObject(GridObject Object)
    {
        _cells = Object.GetBounds();
        _links = Object.GetLinks();
    }

    public static void MoveObject(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in _cells)
        {
            if (context.Grid[cell] != null)
                context.Grid[cell].RemoveObject(Object);
        }

        foreach (var link in _links)
        {
            foreach (var connector in link.Connectors)
            {
                if(context.Grid[connector.Cell] != null)
                    context.Grid[connector.Cell].RemoveConnector(Object);
            }
        }

        PlaceObject(Object);

        _cells = Object.GetBounds();
        _links = Object.GetLinks();
    }
    
    public static void PlaceObject(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in Object.GetBounds())
            if(context.Grid[cell] != null)
                context.Grid[cell].AddObject(Object);

        foreach (var link in Object.GetLinks())
        {
            foreach (var connector in link.Connectors)
            {
                if(context.Grid[connector.Cell] != null)
                    context.Grid[connector.Cell].AddConnector(Object);
            }
        }
    }

    public static bool TryConnection(GridObject first, GridObject second)
    {
        foreach (var firstLink in first.GetLinks())
        {
            foreach (var secondLink in second.GetLinks())
            {
                if (TryLinks(firstLink, secondLink))
                    return true;
            }
        }

        return false;
    }

    public static bool TryLinks(GridObjectLink first, GridObjectLink second)
    {
        var context = Services.Constructor.Context;
        
        foreach (var firstConnector in first.Connectors)
        {
            foreach (var secondConnector in second.Connectors)
            {
                if (context.Grid[firstConnector.Cell].Equals(context.Grid[secondConnector.Cell]) && 
                    TryConnectors(firstConnector, secondConnector))
                    return true;
            }
        }

        return false;
    }

    public static bool TryConnectors(GridObjectLink.Connector first, GridObjectLink.Connector second)
    {
        return (int)first.Place == -(int)second.Place;
    }
}