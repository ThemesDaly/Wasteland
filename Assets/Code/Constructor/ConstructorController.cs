using UnityEngine;

public class ConstructorController : MonoBehaviour
{
    private void Start()
    {
        Services.Constructor.Load();
        Services.UI.Get<ConstructorUI>().Show();
    }
    
    private void OnDestroy()
    {
        Services.UI.Get<ConstructorUI>().Hide();
    }
}