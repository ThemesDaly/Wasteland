using System;
using UnityEngine;

[Serializable]
public class GridObjectConnector
{
    public ConnectorSize Size;
    public ConnectorPlace Place;
    public Vector3 Position;

    private Cell _cell;
    public Cell Cell => _cell;

    public void SetPosition(Vector3 position) => _cell = Cell.Create(position + Position);

    public enum ConnectorSize
    {
        Low = 0,
        Medium = 1,
        Large = 2,
    }
    
    public enum ConnectorPlace : int
    {
        Inside = -1,
        Outside = 1,
    }
}