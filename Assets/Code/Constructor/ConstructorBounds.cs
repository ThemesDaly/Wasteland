using System;
using UnityEngine;

[Serializable]
public sealed class ConstructorBounds
{
    [SerializeField] private Vector3 _size;

    public Vector3 Size
    {
        get
        {
            _size.Vector3ToCell();
            return _size;
        }
    }
}