using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorItemUI : MonoBehaviour
{
    [BoxGroup("UI"), SerializeField] private Image _icon;
    [BoxGroup("UI"), SerializeField] private TMP_Text _name;
    
    [BoxGroup("Buttons"), SerializeField] private BaseButtonUI _buttonCreate;

    private string moduleId;

    public void Init(Module module)
    {
        moduleId = module.ItemId;
        
        if(module.BaseData.Icon != null)
            _icon.sprite = module.BaseData.Icon;

        _name.text = module.BaseData.Name;
        
        _buttonCreate.onClick.AddListener(() => Services.Constructor.Context.AddModule(moduleId));
    }
}