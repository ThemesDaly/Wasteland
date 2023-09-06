using System;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private void Start()
    {
        Services.UI.Get<LobbyUI>().Show();
    }

    private void OnDestroy()
    {
        Services.UI.Get<LobbyUI>().Hide();
    }
}