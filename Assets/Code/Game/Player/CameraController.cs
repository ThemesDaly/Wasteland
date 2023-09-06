using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public class CameraController : BaseManager
{
    [BoxGroup("View"), SerializeField] private Transform _rootNode;
    [BoxGroup("View"), SerializeField] private Transform _pointNode;
    
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private Cinemachine3rdPersonFollow _transposer;

    private Transform _target;
    
    private Camera _camera;

    private Vector3 _shoulderOffset;
    
    public override void Init()
    {
        _camera = Camera.main;
        _transposer = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        if (_target)
        {
            _rootNode.position = _target.position;
            _rootNode.rotation = _target.rotation;
        }
    }

    public void SetFollow(Transform target)
    {
        _target = target;
    }

    public void SetHeight(float value)
    {
        _shoulderOffset.y = -value;
        _shoulderOffset.z = value;
        _transposer.ShoulderOffset = _shoulderOffset;
    }
}