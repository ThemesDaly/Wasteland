using System;
using UnityEngine;

[Serializable]
public sealed class GridObjectBounds
{
    [SerializeField] private Vector3 _size;

    public Vector3 Size
    {
        get
        {
            _size = _size.ToCell();
            
            return _size;
        }
    }
}