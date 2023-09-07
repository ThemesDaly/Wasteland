using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = AssetDefineConstants.CONFIGS + "GameSettings", fileName = "GameSettings")]
public class InputSettings : Config
{
    public InputData Game;
    public InputData Constructor;

    public override void Init() { }
    
    [Serializable]
    public class InputData
    {
        [BoxGroup("Input"), Range(0f, 100f)] public float SpeedMove = 7.5F;
        [BoxGroup("Input"), Min(0f)] public float ClampAreaMovement = 10F;
    
        [BoxGroup("Camera"), Min(0f)] public float RotateVertical = 500F;
        [BoxGroup("Camera"), Min(0f)] public float RotateHorizontal = 500F;
        [BoxGroup("Camera")] public float ClampMinVertical = 20F;
        [BoxGroup("Camera")] public float ClampMaxVertical = 80F;
        [BoxGroup("Camera")] public float ScrollMinDistance = 5F;
        [BoxGroup("Camera")] public float ScrollMaxDistance = 40F;
        [BoxGroup("Camera"), Min(0f)] public float SpeedScroll = 10F;
    }
}