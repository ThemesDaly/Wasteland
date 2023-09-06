using NaughtyAttributes;
using UnityEngine;

public class PhysicsModule : BaseController
{
    [BoxGroup("Physics"), SerializeField] private Transform _joint;
    [BoxGroup("Physics"), SerializeField] private Transform _center;

    public Transform Joint => _joint;
    public Transform Center => _center;
    public Rigidbody Rigidbody => _rigidbody;
    public float Lenght => _collider.height;
    
    private BaseObjectData _data;
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;

    private PhysicsModule _connectBody;

    public override void Init(BaseObjectData data)
    {
        _data = data;
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Setup(CapsuleCollider source)
    {
        source.enabled = false;
        
        var center = source.center;
        center.z = -(source.height / 2);
        _collider.center = center;
        _collider.height = source.height;
        _collider.radius = source.radius;
        _collider.direction = source.direction;
        
        _center.localPosition = Vector3.back * (_collider.height / 2);
        _joint.localPosition = Vector3.back * (_collider.height);
    }

    public void Connect(PhysicsModule target)
    {
        _connectBody = target;
        transform.position = target.Joint.position;
    }

    public void Update()
    {
        if (_connectBody)
            transform.position = _connectBody.Joint.position;
        
        _data.Position = _center.position;
        _data.Direction = transform.forward;
    }
}