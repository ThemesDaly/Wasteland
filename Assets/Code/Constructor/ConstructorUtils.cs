using UnityEngine;

public static class ConstructorUtils
{
    private static Cell[] _cells;
    
    private static GridObjectConnector[] _connectors;

    public static void StartMoveObject(GridObject Object)
    {
        _cells = Object.GetBounds();
        _connectors = Object.GetConnectors();
    }

    public static void ReplaceObject(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in _cells)
        {
            if (context.Grid[cell] != null)
                context.Grid[cell].RemoveObject(Object);
        }

        foreach (var connector in _connectors)
        {
            if(context.Grid[connector.Cell] != null)
                context.Grid[connector.Cell].RemoveConnector(Object);
        }

        PlaceObject(Object);

        _cells = Object.GetBounds();
        _connectors = Object.GetConnectors();
    }
    
    public static void PlaceObject(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in Object.GetBounds())
            if(context.Grid[cell] != null)
                context.Grid[cell].AddObject(Object);

        foreach (var connector in Object.GetConnectors())
        {
            if(context.Grid[connector.Cell] != null)
                context.Grid[connector.Cell].AddConnector(Object);
        }
    }

    public static void ClearObject(GridObject Object)
    {
        var context = Services.Constructor.Context;

        foreach (var cell in Object.GetBounds())
        {
            if (context.Grid[cell] != null)
                context.Grid[cell].RemoveObject(Object);
        }

        foreach (var connector in Object.GetConnectors())
        {
            if(context.Grid[connector.Cell] != null)
                context.Grid[connector.Cell].RemoveConnector(Object);
        }
    }

    public static bool TryConnectionObjects(GridObject Object, GridObject other)
    {
        foreach (var firstConnector in Object.GetConnectors())
        {
            foreach (var secondConnector in other.GetConnectors())
            {
                if (TryConnectors(firstConnector, secondConnector))
                    return true;
            }
        }
        
        return false;
    }
    
    public static bool TryConnectors(GridObjectConnector first, GridObjectConnector second)
    {
        var context = Services.Constructor.Context;
        
        bool isPlace = (int)first.Data.Direction == -(int)second.Data.Direction;
        bool isPosition = context.Grid[first.Cell].Equals(context.Grid[second.Cell]); 
        
        return isPlace && isPosition;
    }

    public static Vector3 GetDirectionForConnector(int direction)
    {
        return GetDirectionForConnector((GridObjectConnector.ConnectorDirection)direction);
    }

    public static Vector3 GetDirectionForConnector(GridObjectConnector.ConnectorDirection direction)
    {
        switch (direction)
        {
            case GridObjectConnector.ConnectorDirection.Up: return Vector3.up;
            case GridObjectConnector.ConnectorDirection.Down: return Vector3.down;
            case GridObjectConnector.ConnectorDirection.Left: return Vector3.left;
            case GridObjectConnector.ConnectorDirection.Right: return Vector3.right;
            case GridObjectConnector.ConnectorDirection.Forward: return Vector3.forward;
            case GridObjectConnector.ConnectorDirection.Backward: return Vector3.back;
        }

        return Vector3.zero;
    }
}