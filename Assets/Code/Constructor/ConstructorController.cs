using UnityEngine;

public class ConstructorController : MonoBehaviour
{
    private void Start()
    {
        Services.Constructor.Load();
        Services.UI.Get<ConstructorUI>().Show();
        Services.UI.Get<ConstructorInventoryUI>().Show();
    }
    
    private void OnDestroy()
    {
        Services.UI.Get<ConstructorUI>().Hide();
        Services.UI.Get<ConstructorInventoryUI>().Hide();
    }
}