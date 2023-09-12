using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = AssetDefineConstants.CONFIGS + "GameConfig", fileName = "GameConfig")]
public class GameConfig : Config
{
    [BoxGroup("Database")] public ModuleAssets Modules;
    
    [BoxGroup("Controller")] public PlayerCamera PlayerCamera;
    [BoxGroup("Controller")] public PlayerGame PlayerGameController;
    [BoxGroup("Controller")] public PlayerConstructor PlayerConstructorController;
    [BoxGroup("Controller")] public GridPlane GridPlane;
    [BoxGroup("Controller")] public ConnectorView ConnectorView;

    [BoxGroup("Layer")] public LayerMask LayerGround;
    [BoxGroup("Layer")] public LayerMask LayerGrid;
    [BoxGroup("Layer")] public LayerMask LayerGridObject;

    public override void Init()
    {
        
    } 
}