using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.AI;

public static class ExtensionMethods
{

    #region MATH

    public static float Map(this float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public static float Map01In(this float x, float out_min, float out_max)
    {
        return x.Map(0, 1, out_min, out_max);
    }

    public static float Map01Out(this float x, float in_min, float in_max)
    {
        return x.Map(in_min, in_max, 0, 1);
    }

    public static float MapClamped(this float x, float in_min, float in_max, float out_min, float out_max)
    {
        return ((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min).Clamp(out_min, out_max);
    }

    public static float Abs(this float value)
    {
        return Mathf.Abs(value);
    }

    public static float Clamp(this float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    public static float Clamp01(this float value)
    {
        return Mathf.Clamp01(value);
    }

    public static float Average(this float a, params float[] arr)
    {
        Array.Resize(ref arr, arr.Length + 1);

        arr[arr.Length -1] = a;

        for (int i = 1; i < arr.Length; i++)
            arr[i] = arr[i - 1];

        return arr.Average();
    }

    public static float Average(this List<float> list)
    {
        return list.ToArray().Average();
    }

    public static float Average(this float[] array)
    {
        var value = 0f;

        foreach (var a in array)
            value += a;

        value /= array.Length;

        return value;
    }

    public static float Sign(this float value)
    {
        return Mathf.Sign(value);
    }

    public static float Min(this float a, float b)
    {
        return Mathf.Min(a, b);
    }

    public static float Max(this float a, float b)
    {
        return Mathf.Max(a, b);
    }

    public static int Min(this int a, int b)
    {
        return Mathf.Min(a, b);
    }

    public static int Max(this int a, int b)
    {
        return Mathf.Max(a, b);
    }

    public static float Revert01(this float a)
    {
        return 1 - a;
    }

    public static int RoundToInt(this float a)
    {
        return Mathf.RoundToInt(a);
    }

    public static int FloorToInt(this float a)
    {
        return Mathf.FloorToInt(a);
    }
    
    public class SortToLarge : IComparer<float>
    {
        public static IComparer<float> Comparer => new SortToLarge();

        public int Compare(float a, float b)
        {
            return Comparer<float>.Default.Compare(b, a);
        }
    }

    public class SortToSmall : IComparer<float>
    {
        public static IComparer<float> Comparer => new SortToLarge();

        public int Compare(float a, float b)
        {
            return Comparer<float>.Default.Compare(a, b);
        }
    }

    #endregion

    #region VECTORS

    public static Vector3 Average(this Vector3 a, params Vector3[] arr)
    {
        Array.Resize(ref arr, arr.Length + 1);

        arr[arr.Length - 1] = a;

        for (int i = 1; i < arr.Length; i++)
            arr[i] = arr[i - 1];

        return arr.Average();
    }

    public static Vector3 Average(this List<Vector3> list)
    {
        return list.ToArray().Average();
    }

    public static Vector3 Average(this Vector3[] array)
    {
        var x = array.Sum((a) => a.x) / array.Length;
        var y = array.Sum((a) => a.y) / array.Length;
        var z = array.Sum((a) => a.z) / array.Length;

        return new Vector3(x.Average(), y.Average(), z.Average());
    }

    public static Vector2 XY(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
    
    public static Vector3 WithX(this Vector3 v, float x = 0f)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 WithY(this Vector3 v, float y = 0f)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 WithZ(this Vector3 v, float z = 0f)
    {
        return new Vector3(v.x, v.y, z);
    }
    
    public static Vector3 WithZ(this Vector2 v, float z = 0f)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 WithX(this Vector2 v, float x = 0f)
    {
        return new Vector2(x, v.y);
    }

    public static Vector2 WithY(this Vector2 v, float y = 0f)
    {
        return new Vector2(v.x, y);
    }
    
    #endregion

    #region QUATERNIONS

    public static Quaternion Euler(this Vector3 a)
    {
        return Quaternion.Euler(a);
    }

    public static Quaternion SumEulers(this Quaternion a, params Quaternion[] b)
    {
        var sum = Vector3.zero;

        sum += a.eulerAngles;

        foreach (var arr in b)
            sum += arr.eulerAngles;

        return (sum / (b.Length + 1)).Euler();
    }

    #endregion

    #region TRANSFORMS

    public static Transform ResetLocal(this Transform a)
    {
        return a.Reset(a.parent);
    }

    public static Transform Reset(this Transform a, Transform parent = null)
    {
        a.position = parent ? parent.position : Vector3.zero;
        a.rotation = parent ? parent.rotation : Quaternion.identity;
        a.localScale = Vector3.one;

        return a;
    }

    #endregion

    #region LAYERMASK
    public static int ToLayer(this LayerMask layerMask)
    {
        var layerNumber = 0;
        var layer = layerMask.value;

        while (layer > 0)
        {
            layer >>= 1;
            layerNumber++;
        }

        return layerNumber - 1;
    }
    
    public static bool IsInLayerMask(this  LayerMask layerMask, GameObject obj)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    #endregion

    public static string Format(this string a, params object[] arg)
    {
        return string.Format(a, arg);
    }

    public static void SetTargetRotation(this ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, bool worldSpace)
    {
        var right = joint.axis;
        var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
        var up = Vector3.Cross(forward, right).normalized;
        Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

        Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

        if (worldSpace)
            resultRotation *= startRotation * Quaternion.Inverse(targetRotation);
        else
            resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;


        resultRotation *= worldToJointSpace;

        joint.targetRotation = resultRotation;
    }

    public static bool CalculatePath(this Vector3 start, Vector3 end, ref NavMeshPath navMeshPath)
    {
        return NavMesh.CalculatePath(start, end, NavMesh.AllAreas, navMeshPath);
    }
}
