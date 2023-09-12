using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public readonly Cell Cell;

    public List<GridObject> Objects { get; private set; }
    
    public List<GridObject> Connectors { get; private set; }

    public GridCell(int x, int y, int z)
    {
        Cell = Cell.Create(x, y, z);
        Objects = new List<GridObject>();
        Connectors = new List<GridObject>();
    }

    public bool IsEmpty => Objects.Count == 0;

    public void AddObject(GridObject Object) => Objects.Add(Object);

    public void RemoveObject(GridObject Object) => Objects.Remove(Object);

    public bool ContainsObject(GridObject Object) => Objects.Contains(Object);

    public void AddConnector(GridObject Object) => Connectors.Add(Object);
    
    public void RemoveConnector(GridObject Object) => Connectors.Remove(Object);
    
    public bool ContainsConnector(GridObject Object) => Connectors.Contains(Object);
}