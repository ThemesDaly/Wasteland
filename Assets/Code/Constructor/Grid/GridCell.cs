public class GridCell
{
    public Cell Position;

    public bool IsBusy;
    
    public GridCell(int x, int y, int z)
    {
        Position = Cell.Create(x, y, z);
    }
}