using NaughtyAttributes;
using UnityEngine;

public class ConstructorInventoryUI : BaseCanvasUI
{
    [BoxGroup("UI"), SerializeField] private ConstructorItemUI _masterItem;
    [BoxGroup("UI"), SerializeField] private Transform _holder;
    
    public override void Init(Canvas canvas)
    {
        base.Init(canvas);

        var items = Configs.Get<GameConfig>().Modules.Items;

        foreach (var item in items)
        {
            CreateItem(item);
        }
    }

    private ConstructorItemUI CreateItem(Module module)
    {
        var instance = Instantiate(_masterItem, _holder);
        instance.Init(module);

        return instance;
    }
}