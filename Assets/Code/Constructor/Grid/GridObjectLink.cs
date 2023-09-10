using System;
using UnityEngine;

[Serializable]
public struct GridObjectLink
{
    [SerializeField] private Connector[] _connectors;

    public Connector[] Connectors => _connectors;

    public void RefreshPosition(Vector3 root)
    {
        foreach (var connector in _connectors)
            connector.SetPosition(root + connector.Position);
    }

    [Serializable]
    public class Connector
    {
        public ConnectorSize Size;
        public ConnectorPlace Place;
        public Vector3 Position;

        private Cell _cell;
        public Cell Cell => _cell;

        public void SetPosition(Vector3 position) => _cell = Cell.Create(position);

        public enum ConnectorSize
        {
            Low = 0,
            Medium = 1,
            Large = 2,
        }
    
        public enum ConnectorPlace : int
        {
            Inside = -1,
            Outside = 1,
        }
    }
}