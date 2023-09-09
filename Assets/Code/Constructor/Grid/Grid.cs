using UnityEngine;

public sealed class Grid
{
    public static int GRID_SIZE_X { get; private set; }
    public static int GRID_SIZE_Z { get; private set; }
    public const int GRID_SIZE_Y = 10;
    
    private readonly GridCell[][][] grid;
    
    public GridCell this[Cell cell] => IsInGrid(cell.Position) ? grid[cell.X][cell.Y][cell.Z] : null;

    public Grid(int width, int lenght)
    {
        GRID_SIZE_X = width;
        GRID_SIZE_Z = lenght;
        
        grid = new GridCell[GRID_SIZE_X][][];
        
        for (int y = 0; y < GRID_SIZE_X; y++)
        {
            grid[y] = new GridCell[GRID_SIZE_Y][];

            for (int z = 0; z < GRID_SIZE_Y; z++)
                grid[y][z] = new GridCell[GRID_SIZE_Z];
        }
    }

    public void Init()
    {
        for (int x = 0; x < GRID_SIZE_X; x++)
        {
            for (int y = 0; y < GRID_SIZE_Y; y++)
            {
                for (int z = 0; z < GRID_SIZE_Z; z++)
                {
                    grid[x][y][z] = new GridCell(x, y, z);
                }
            }
        }
    }

    private bool IsInGrid(Vector3 v)
    {
        return v.x >= 0 && v.x < GRID_SIZE_X &&
               v.z >= 0 && v.z < GRID_SIZE_Z &&
               v.y >= 0 && v.y < GRID_SIZE_Y;
    }
}