using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : ICoreSystem
{
    public PlayerCamera PlayerCamera { get; private set; }
    
    public PlayerGame PlayerGame { get; private set; }

    private static readonly Dictionary<Type, BaseMonoController> _controllers = new Dictionary<Type, BaseMonoController>();

    private bool _isInit;

    public void Init()
    {
        _isInit = true;
    }

    public void DeInit()
    {
        _isInit = false;
    }

    public void Execute()
    {
        if(!_isInit)
            return;
    }

    public void SpawnPlayer()
    {
        if(PlayerGame != null)
            GameObject.Destroy(PlayerGame.gameObject);
        
        if(PlayerCamera != null)
            GameObject.Destroy(PlayerCamera.gameObject);
        
        PlayerCamera = ContextUtils.SpawnManager<PlayerCamera>(_controllers, Configs.Get<GameConfig>().PlayerCamera);
        PlayerGame = ContextUtils.SpawnManager<PlayerGame>(_controllers, Configs.Get<GameConfig>().PlayerGameController);
    }
}