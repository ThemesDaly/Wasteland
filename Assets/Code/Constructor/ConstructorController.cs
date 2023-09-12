using System;
using UnityEngine;

public class ConstructorController : MonoBehaviour
{
    private void Start()
    {
        Services.Constructor.Load();
        Services.UI.Get<ConstructorUI>().Show();
        Services.UI.Get<ConstructorInventoryUI>().Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Services.Scene.LoadSceneCoroutine(CoreConstants.Scene.LOBBY_LEVEL_INDEX);
            Services.Constructor.Exit();
        }
    }
}