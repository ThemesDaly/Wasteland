using UnityEngine;

public sealed class ConstructorManager
{
    private GridObject _targetObject;

    private GridObject _grabObject;
    
    private Ray _ray;

    private RaycastHit _hit;

    private LayerMask _objectLayer;
    
    private LayerMask _gridLayer;
        
    private ConstructorContext _context;
    
    private ConstructorTools _tools;

    public ConstructorManager(ConstructorContext context, ConstructorTools tools)
    {
        _context = context;
        _tools = tools;
        _gridLayer = Configs.Get<GameConfig>().LayerGrid;
        _objectLayer = Configs.Get<GameConfig>().LayerGridObject;
    }

    ~ConstructorManager()
    {
        
    }

    public void Execute()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        PointSystem();
        GrabSystem();
        HotkeySystem();
    }

    private void PointSystem()
    {
        if(_grabObject)
            return;
        
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _objectLayer))
        {
            if(_targetObject == null)
            {
                _targetObject = _hit.transform.GetComponent<GridObject>();
                PointIn(_targetObject);
            }
            else if (_targetObject && !_hit.transform.Equals(_targetObject.transform))
            {
                PointOut(_targetObject);
                _targetObject = _hit.transform.GetComponent<GridObject>();
                PointIn(_targetObject);
            }

            ServicesEvents.Constructor.CursorCell(_hit.point);
        }
        else if (_targetObject)
        {
            PointOut(_targetObject);
            _targetObject = null;
        }
    }

    private void GrabSystem()
    {
        if (_targetObject && _grabObject == null && Input.GetMouseButton(0))
        {
            _grabObject = _targetObject;
            _tools.Drag(_grabObject);
        }
        else if(_grabObject && !Input.GetMouseButton(0))
        {
            _tools.Drop(_grabObject);
            _grabObject = null;
        }
        else if(_grabObject && Physics.Raycast(_ray, out _hit, Mathf.Infinity, _gridLayer))
        {
            _tools.MoveObject(_grabObject, _hit.point);
        }
    }

    private void HotkeySystem()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ServicesEvents.Constructor.FloorDown();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            ServicesEvents.Constructor.FloorUp();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            if(_grabObject)
                _context.RemoveModule(_grabObject);
        }
    }
    
    private void PointIn(GridObject Object)
    {
        ServicesEvents.Constructor.PointIn(Object);
    }
    
    private void PointOut(GridObject Object)
    {
        ServicesEvents.Constructor.PointOut(Object);
    }
}