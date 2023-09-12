using System;
using UnityEngine;
using UnityEditor;

partial class GridObject : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        DrawTransform();
        DrawConnectors(GizmoConstants.Orange.WithAlpha(0.5F), false);
        DrawBound(GizmoConstants.Blue.WithAlpha(1f), false);
    }

    private void OnDrawGizmosSelected()
    {
        DrawCells(GizmoConstants.Blue.WithAlpha(0.5F));
        DrawBound(GizmoConstants.Blue.WithAlpha(0.5F), true);
        DrawConnectors(GizmoConstants.Orange.WithAlpha(1F), true);
    }

    private void DrawTransform()
    {
        Gizmos.color = GizmoConstants.ColorTransform;
        Gizmos.DrawMesh(GizmoConstants.TransformVector3, transform.position, Quaternion.identity, Vector3.one);
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

    private void DrawConnectors(Color color, bool isSelected)
    {
        if(_connectors == null)
            return;
        
        foreach (var connector in _connectors)
        {
            Gizmos.color = connector.Data.Required == GridObjectConnector.ConnectorRequired.Required ? GizmoConstants.ColorConnectorRequiment : GizmoConstants.ColorConnectorDontRequiment;

            if(isSelected)
                Gizmos.DrawCube(transform.position + connector.Position, Vector3.one * 1.025F);
            else
                Gizmos.DrawWireCube(transform.position + connector.Position, Vector3.one * 1.025F);        
        }
    }
}