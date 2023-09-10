using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorItemUI : MonoBehaviour
{
    [BoxGroup("UI"), SerializeField] private Image _icon;
    [BoxGroup("UI"), SerializeField] private TMP_Text _name;

    public void Init(Sprite icon, string name)
    {
        if(icon != null)
            _icon.sprite = icon;

        _name.text = name;
    }
}