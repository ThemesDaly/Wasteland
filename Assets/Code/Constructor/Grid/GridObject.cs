using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridObject : MonoBehaviour
{
    private const float RATE_LERP_MOVE = 25f;
    
    [SerializeField] private GridObjectData _data;
    [SerializeField] private GridObjectView _view;
    [SerializeField] private GridObjectBounds _bounds;
    [SerializeField] private GridObjectLink[] _links;

    public GridObjectData Data => _data;

    private Vector3 center;

    private BoxCollider _collider;

    public void Init(Transform target)
    {
        _collider = gameObject.AddComponent<BoxCollider>();

        center = _bounds.Size / 2;
        center.x -= 0.5F;
        center.y -= 0.5F;
        center.z = -center.z + 0.5F;

        _collider.center = center;
        _collider.size = _bounds.Size;
        
        _data.SetTarget(target);
        _view.Init(target);
        
        target.position = transform.position;

        ServicesEvents.Constructor.OnPointIn += PointIn;
        ServicesEvents.Constructor.OnPointOut += PointOut;
    }

    private void Update()
    {
        if (_data.Target == null)
            return;
        
        _data.Target.position = Vector3.Lerp(_data.Target.position, transform.position, RATE_LERP_MOVE * Time.unscaledDeltaTime);
        _view.SetResult(_data.result);
    }

    private void OnMouseDown()
    {
        Services.Constructor.Context.Drag(this);
    }

    private void OnMouseEnter()
    {
        Services.Constructor.Context.PointIn(this);
    }

    private void OnMouseExit()
    {
        Services.Constructor.Context.PointOut(this);
    }
    
    private void PointIn(GridObject Object)
    {
        if(!Object.Equals(this))
            return;
        
        _view.SetActive(true);
    }

    private void PointOut(GridObject Object)
    {
        if(!Object.Equals(this))
            return;   
        
        _view.SetActive(false);
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public Cell[] GetBounds()
    {
        int cellCount = (int)(_bounds.Size.x * _bounds.Size.z * _bounds.Size.y);
        Cell[] cells = new Cell[cellCount];

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

    public GridObjectLink[] GetLinks()
    {
        foreach (var link in _links)
            link.RefreshPosition(transform.position + center);

        return _links;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        DrawLinks(new Color(1F, 0.3F, 0F, 0.5F), false);
        DrawBound(new Color(0.4F, 0.8F, 1F, 1F), false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawCells(new Color(0.4F, 0.8F, 1F, 0.5F));
        DrawBound(new Color(0.4F, 0.8F, 1F, 0.5F), true);
        DrawLinks(new Color(1F, 0.3F, 0F, 1F), true);
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
            Gizmos.DrawCube(pivot, _bounds.Size * 1.01f);
        else
            Gizmos.DrawWireCube(pivot, _bounds.Size * 1.01f);
    }

    private void DrawCells(Color color)
    {
        Gizmos.color = color;

        var cells = GetBounds();

        foreach (var cell in cells)
            Gizmos.DrawCube(cell.Position, Vector3.one * 0.95F);
    }

    private void DrawLinks(Color color, bool isSelected)
    {
        foreach (var link in _links)
        {
            foreach (var connector in link.Connectors)
            {
                // Gizmos.color = connector.Place == GridObjectLink.Connector.ConnectorPlace.Inside ? Color.cyan : Color.cyan / 2;
                Gizmos.color = color;

                if(isSelected)
                    Gizmos.DrawCube(transform.position + connector.Position, Vector3.one);
                else
                    Gizmos.DrawWireCube(transform.position + connector.Position, Vector3.one);   
            }
        }
    }

#endif
    
}