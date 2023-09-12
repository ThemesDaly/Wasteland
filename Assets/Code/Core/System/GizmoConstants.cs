using UnityEngine;

public static class GizmoConstants
{
    public static Color Blue = new Color(0.4F, 0.8F, 1F);
    public static Color Orange = new Color(1F, 0.3F, 0F);
    public static Color ColorTransform = new Color(1F, 0.3F, 0F);

    private static Mesh _transformVector3;
    public static Mesh TransformVector3 => _transformVector3 == null ? _transformVector3 = Resources.Load<Mesh>("Gizmo/TransformVector3") : _transformVector3;
    
    private static Mesh _transformQuaternion;
    public static Mesh TransformQuaternion => _transformQuaternion == null ? _transformQuaternion = Resources.Load<Mesh>("Gizmo/TransformQuaternion") : _transformQuaternion;
    
    private static Mesh _arrow;
    public static Mesh Arrow => _arrow == null ? _arrow = Resources.Load<Mesh>("Gizmo/Arrow") : _arrow;
}