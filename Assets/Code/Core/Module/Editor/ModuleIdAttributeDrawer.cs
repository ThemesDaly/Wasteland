using UnityEditor;

[CustomPropertyDrawer(typeof(ModuleIdAttribute))]
public sealed class ModuleIdAttributeDrawer : BaseStringIdAttributeDrawer
{
    public override IStringIdItem GetItem(int index) => Configs.Get<GameConfig>().Modules.Items[index];
    public override int Count() => Configs.Get<GameConfig>().Modules.Items.Count;
}