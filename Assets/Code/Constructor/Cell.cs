using UnityEngine;

public class Cell
{
    public static Vector3 Create(Vector3 position)
    {
        return position.NormalizeD();
    }
}