using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public readonly Cell Cell;

    public GridCell(int x, int y, int z)
    {
        Cell = Cell.Create(x, y, z);
        Objects = new List<GridObject>();
    }

    private List<GridObject> Objects;
    
    public bool IsEmpty => Objects.Count == 0;

    public void Add(GridObject Object) => Objects.Add(Object);
    
    public void Remove(GridObject Object) => Objects.Remove(Object);

    public bool Contains(GridObject Object) => Objects.Contains(Object);
}