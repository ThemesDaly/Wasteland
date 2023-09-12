using System;
using UnityEngine;

[Serializable]
public class GridObjectConnector
{
    public ConnectorRequired Required;
    public ConnectorDirection Direction;
    public Vector3 Position;

    private Cell _cell;
    public Cell Cell => _cell;

    public void SetPosition(Vector3 position)
    {
        _cell = Cell.Create(position + Position);
    }

    public enum ConnectorDirection : int
    {
        Up = -1,
        Down = 1,
        Left = -2,
        Right = 2,
        Forward = 3,
        Backward = -3
    }
    
    public enum ConnectorRequired
    {
        DontRequired = 0,
        Required = 1
    }
}