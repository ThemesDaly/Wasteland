using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPropertyUI : MonoBehaviour
{
    [BoxGroup("UI"), SerializeField] private Image _icon;
    [BoxGroup("UI"), SerializeField] private TMP_Text _title;
    [BoxGroup("UI"), SerializeField] private TMP_Text _value;

    [BoxGroup("Content"), SerializeField] private Sprite _iconSoruce;

    private void Awake()
    {
        if(_iconSoruce)
            _icon.sprite = _iconSoruce;
    }

    public void DisplayTitle(string text) => _title.text = text;
    
    public void DisplayValue(string text) => _value.text = text;
}