using EPOOutline;
using UnityEngine;

public class ViewOutline : MonoBehaviour, IView
{
    private Outlinable _outlinable;
    
    public void Init()
    {
        _outlinable = gameObject.AddComponent<Outlinable>();
        _outlinable.AddAllChildRenderersToRenderingList(RenderersAddingMode.All);
        
        TurnOff();
    }

    public void TurnOn()
    {
        _outlinable.enabled = true;
    }

    public void TurnOff()
    {
        _outlinable.enabled = false;
    }
}