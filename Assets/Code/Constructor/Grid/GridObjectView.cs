using System;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

[Serializable]
public class GridObjectView
{
    [SerializeField] private Outlinable _normal;
    [SerializeField] private Outlinable _successful;
    [SerializeField] private Outlinable _failed;

    public void Init(Transform target)
    {
        List<Renderer> renderers = new List<Renderer>();

        foreach (var renderer in target.GetComponentsInChildren<Renderer>(true))
            renderers.Add(renderer);
        
        _successful.enabled = false;
        _failed.enabled = false;

        _normal.AddCustomToRenderingList(renderers.ToArray());
        _successful.AddCustomToRenderingList(renderers.ToArray());
        _failed.AddCustomToRenderingList(renderers.ToArray());
        
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        _normal.gameObject.SetActive(value);
    }

    public void SetResult(GridObjectData.Result result)
    {
        _successful.enabled = result == GridObjectData.Result.Successfully;
        _failed.enabled = result == GridObjectData.Result.Failed;
    }
}