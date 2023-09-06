using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = AssetDefineConstants.CONFIGS + "GameConfig", fileName = "GameConfig")]
public class GameConfig : Config
{
    [BoxGroup("Prefabs")] public CameraController CameraController;
    [BoxGroup("Prefabs")] public PlayerController PlayerController;

    [BoxGroup("World")] public LayerMask LayerGround;

    public ModuleAssets Modules;

    public override void Init()
    {
        
    } 
}