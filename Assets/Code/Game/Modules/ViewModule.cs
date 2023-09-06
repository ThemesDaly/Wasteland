using UnityEngine;

public class ViewModule : BaseView
{
    [SerializeField] private CapsuleCollider _collider;

    public CapsuleCollider Collider => _collider;
}