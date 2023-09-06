using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    public virtual void Init(BaseObjectData data)
    {
        data.OnChanged += OnChanged;
    }

    public virtual void OnChanged(BaseObjectData.PropertyFieldData property, object value)
    {
        if (property == BaseObjectData.PropertyFieldData.Position)
            transform.position = (Vector3)value;
        else if (property == BaseObjectData.PropertyFieldData.Direction)
            transform.forward = (Vector3)value;
    }
}