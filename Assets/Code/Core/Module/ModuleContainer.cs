using UnityEngine;

public class ModuleContainer : MonoBehaviour
{
    [SerializeField] private GridObject _object;
    [SerializeField] private Transform _view;

    public GridObject Object => _object;
    public Transform View => _view;

    public void Unpack()
    {
        _object.transform.SetParent(null);
        _object.Init(_view);
        
        _view.SetParent(null);
        
        Destroy(gameObject);
    }
}