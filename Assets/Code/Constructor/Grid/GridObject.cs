using System;
using UnityEngine;

public partial class GridObject : MonoBehaviour
{
    private const float RATE_LERP_MOVE = 25f;
    
    [SerializeField] private GridObjectData _data;
    [SerializeField] private GridObjectView _view;
    [SerializeField] private GridObjectBounds _bounds;
    [SerializeField] private GridObjectConnector[] _connectors;

    public GridObjectData Data => _data;

    private Vector3 center;

    private BoxCollider _collider;

    private Transform _transform;

    public void Init(Transform view)
    {
        _collider = gameObject.AddComponent<BoxCollider>();

        center = _bounds.Size / 2;
        center.x -= 0.5F;
        center.y -= 0.5F;
        center.z = -center.z + 0.5F;

        _collider.center = center;
        _collider.size = _bounds.Size;
        
        _data.SetTarget(view);
        _view.Init(view);

        foreach (var connector in _connectors)
        {
            var connectorView = Instantiate(Configs.Get<GameConfig>().ConnectorView, transform);
            connectorView.transform.position = transform.position + connector.Position;
            connectorView.Init();   
        }
        
        view.position = transform.position;

        ServicesEvents.Constructor.OnPointIn += PointIn;
        ServicesEvents.Constructor.OnPointOut += PointOut;
    }
    
    private void OnDestroy()
    {
        ServicesEvents.Constructor.OnPointIn -= PointIn;
        ServicesEvents.Constructor.OnPointOut -= PointOut;
    }
    
    public void MoveTo(Vector3 position)
    {
        transform.position = position.ToCell();
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

    public GridObjectConnector[] GetConnectors()
    {
        foreach (var connector in _connectors)
            connector.SetPosition(transform.position + center);

        return _connectors;
    }

    private void Update()
    {
        _data.Target.position = Vector3.Lerp(_data.Target.position, transform.position, RATE_LERP_MOVE * Time.unscaledDeltaTime);
        _view.SetResult(_data.result);
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
}