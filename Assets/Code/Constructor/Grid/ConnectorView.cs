using UnityEngine;

public class ConnectorView : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Transform _arrow;
    
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetDirectionContent(Vector3 direction) => _content.forward = direction;
    
    public void SetDirectionArrow(Vector3 direction) => _arrow.forward = direction;
}