using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ModuleBaseData))]
public class ModuleBaseDataDrawer : PropertyDrawer
{
    private const float PROPERTY_HEIGHT = 20;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // return PROPERTY_HEIGHT * 14 + EditorGUI.GetPropertyHeight(property.FindPropertyRelative(nameof(ModuleBaseData.Award)));
        return PROPERTY_HEIGHT * 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var idProperty = property.FindPropertyRelative(nameof(ModuleBaseData.Id));
        var viewProperty = property.FindPropertyRelative(nameof(ModuleBaseData.Container));
        var nameProperty = property.FindPropertyRelative(nameof(ModuleBaseData.Name));
        var iconProperty = property.FindPropertyRelative(nameof(ModuleBaseData.Icon));

        var size = new Vector2(position.width, PROPERTY_HEIGHT);
        var idRect = new Rect(position.position, size);
        var viewRect = new Rect(idRect.position + Vector2.up * PROPERTY_HEIGHT, size);
        var nameRect = new Rect(viewRect.position + Vector2.up * PROPERTY_HEIGHT, size);
        var iconRect = new Rect(nameRect.position + Vector2.up * PROPERTY_HEIGHT, size);

        EditorGUI.HelpBox(idRect, $"Id {idProperty.stringValue}", MessageType.None);
        EditorGUI.PropertyField(viewRect, viewProperty);
        EditorGUI.PropertyField(nameRect, nameProperty);
        EditorGUI.PropertyField(iconRect, iconProperty);
    }
}
