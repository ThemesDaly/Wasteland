using UnityEngine;

public class GridPlane : BaseMonoController
{
    [SerializeField] private Transform _content;
    [SerializeField] private MeshRenderer _renderer;
    
    public override void Init()
    {
        _content.localScale = new Vector3(Grid.GRID_SIZE_X, 1, Grid.GRID_SIZE_Z);
        _content.position = new Vector3(Grid.GRID_SIZE_X / 2F - 0.5F, 0, Grid.GRID_SIZE_Z / 2F - 0.5F);

        _renderer.material.mainTextureScale = new Vector2(Grid.GRID_SIZE_X, Grid.GRID_SIZE_Z);
    }

    public override void DeInit()
    {
        
    }

    public override void Execute()
    {
        
    }
}