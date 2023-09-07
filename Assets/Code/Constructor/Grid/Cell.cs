using System;
using UnityEngine;

public struct Cell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public static Cell Create(int x, int y, int z) => new Cell(x, y, z);
    public static Cell Create(Vector3 v) => new Cell(v);

    public Vector3 Position => new Vector3(X, Y, Z);

    public Cell(float x, float y, float z)
    {
        X = (int)Math.Round(x);
        Y = (int)Math.Round(y);
        Z = (int)Math.Round(z);
    }
    
    public Cell(Vector3 vector)
    {
        var cellVector = vector.Vector3ToCell();

        X = (int)Math.Round(cellVector.x);
        Y = (int)Math.Round(cellVector.y);
        Z = (int)Math.Round(cellVector.z);
    }
    
    public bool Equals(Cell cell)
    {
        return X == cell.X && Y == cell.Y && Z == cell.Z;
    }

    public override string ToString()
    {
        return $"X:{X} Y:{Y} Z:{Z}";
    }
}