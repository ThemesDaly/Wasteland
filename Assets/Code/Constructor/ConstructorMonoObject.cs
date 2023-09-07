using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ConstructorMonoObject : MonoBehaviour
{
    [SerializeField] private ConstructorBounds _bounds;

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        DrawArea(Color.black, false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawArea(new Color(1F, 1F, 0F, 0.5F), true);
    }
    
    private void DrawArea(Color color, bool isSelected)
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

#endif
    
}