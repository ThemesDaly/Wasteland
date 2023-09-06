using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class VehicleController : BaseController
{
    private NavMeshAgent _agent;
    private VehicleData _data;

    public override void Init(BaseObjectData data)
    {
        base.Init(data);
        _agent = GetComponent<NavMeshAgent>();
        _data = data as VehicleData;
    }

    public void Update()
    {
        _data.Position = transform.position;
        _data.Direction = transform.forward;
    }

    public void MoveToPoint(Vector3 worldPosition)
    {
        _agent.SetDestination(worldPosition);
    }
}