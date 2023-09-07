using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridObject : MonoBehaviour
{
    [SerializeField] private ConstructorBounds _bounds;

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public Cell[] GetBounds()
    {
        int cellCount = (int)(_bounds.Size.x * _bounds.Size.z * _bounds.Size.y);
        Cell[] cells = new Cell[cellCount];
        
        Vector3 center = transform.position;

        for (int x = 0, count = 0; x < _bounds.Size.x; x++)
        {
            for (int y = 0; y < _bounds.Size.y; y++)
            {
                for (int z = 0; z < _bounds.Size.z; z++)
                {
                    var vector = transform.TransformPoint(new Vector3(x, y, -z));
                    var cell = Cell.Create(vector);
                    cells[count++] = cell;
                }
            }
        }

        return cells;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        DrawBound(Color.black, false);
    }

    private void OnDrawGizmosSelected()
    {
        var color = new Color(1F, 1F, 0F, 0.5F); 
        DrawCells(color);
        DrawBound(color, true);
    }
    
    private void DrawBound(Color color, bool isSelected)
    {
        Gizmos.color = color;
        
        Gizmos.matrix = transform.localToWorldMatrix;
        
        Vector3 pivot = _bounds.Size / 2;
        pivot.x -= 0.5F;
        pivot.y -= 0.5F;
        pivot.z = -pivot.z + 0.5F;

        if(isSelected)
            Gizmos.DrawCube(pivot, _bounds.Size);
        else
            Gizmos.DrawWireCube(pivot, _bounds.Size);
    }

    private void DrawCells(Color color)
    { ;
        Gizmos.color = color;

        var cells = GetBounds();

        foreach (var cell in cells)
            Gizmos.DrawCube(cell.Position, Vector3.one / 2);
    }

#endif
    
}