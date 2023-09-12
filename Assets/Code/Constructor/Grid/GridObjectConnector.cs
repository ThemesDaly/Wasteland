using System;
using UnityEngine;

[Serializable]
public class GridObjectConnector
{
    [SerializeField] private ConnectorData _data;
    public ConnectorData Data => _data;
    
    [SerializeField] private Vector3 _position;
    public Vector3 Position => _position;

    private Cell _cell;
    public Cell Cell => _cell;

    private GridObject _object;

    private Vector3 _pivot;

    private ConnectorView _view;
    public ConnectorView View => _view;

    public void Init(GridObject Object, Vector3 pivot)
    {
        _object = Object;
        _pivot = pivot;
        
        _view = GameObject.Instantiate(Configs.Get<GameConfig>().ConnectorView, Object.transform);
        _view.transform.position = Object.transform.position + _position;
        _view.SetActive(false);
        _view.SetDirectionContent(ConstructorUtils.GetDirectionForConnector(_data.Direction));

        ServicesEvents.Constructor.OnShowConnector += Show;
        ServicesEvents.Constructor.OnHideConnector += Hide;
    }

    public void CellPreparation()
    {
        _cell = Cell.Create(_object.transform.position + _position);
    }

    public void DeInit()
    {
        ServicesEvents.Constructor.OnShowConnector -= Show;
        ServicesEvents.Constructor.OnHideConnector -= Hide;   
    }

    public void Show(GridObject Object, ConnectorData data)
    {
        if(!_data.Size.Equals(data.Size))
            return;
        
        if(Object.Equals(_object))
            _view.SetDirectionArrow(ConstructorUtils.GetDirectionForConnector(_data.Direction));
        else
            _view.SetDirectionArrow(ConstructorUtils.GetDirectionForConnector(-(int)_data.Direction));
        
        _view.SetActive(true);
    }

    public void Hide(GridObject Object, ConnectorData data)
    {
        if(!_data.Size.Equals(data.Size))
            return;
        
        _view.SetActive(false);
    }

    [Serializable]
    public struct ConnectorData
    {
        public ConnectorSize Size;
        public ConnectorRequired Required;
        public ConnectorDirection Direction;
    }

    public enum ConnectorDirection : int
    {
        Up = -1,
        Down = 1,
        Left = -2,
        Right = 2,
        Forward = 3,
        Backward = -3
    }
    
    public enum ConnectorRequired
    {
        DontRequired = 0,
        Required = 1
    }
    
    public enum ConnectorSize
    {
        Module = 0,
        Interior = 1,
        Exterior = 2
    }
}