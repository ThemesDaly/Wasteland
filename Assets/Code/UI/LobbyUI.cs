using System;
using NaughtyAttributes;
using UnityEngine;

public class LobbyUI : BaseCanvasUI
{
    [BoxGroup("Buttons"), SerializeField] private BaseButtonUI _buttonGame;
    [BoxGroup("Buttons"), SerializeField] private BaseButtonUI _buttonConstructor;

    public override void Init(Canvas canvas)
    {
        base.Init(canvas);
        
        _buttonGame.onClick.AddListener(OnGame);
        _buttonConstructor.onClick.AddListener(OnConstructor);
    }

    private void OnGame()
    {
        Services.Scene.LoadSceneCoroutine(CoreConstants.Scene.GAME_LEVEL_INDEX);
    }

    private void OnConstructor()
    {
        Services.Scene.LoadSceneCoroutine(CoreConstants.Scene.CONSTRUCTOR_LEVEL_INDEX);
    }
}