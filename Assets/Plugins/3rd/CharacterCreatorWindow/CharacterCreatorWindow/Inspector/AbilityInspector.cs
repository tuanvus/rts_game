using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ability))]
public class AbilityInspector : Editor
{

    float damage = 25;
    AbilityEnum ability;


    public override void OnInspectorGUI()
    {
        Ability character = (Ability)target;

        ability = character.abilityType;
        damage = character.damage;

        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperLeft;
        style.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Character Stats", style, GUILayout.ExpandWidth(true));

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage : ");
        damage = EditorGUILayout.Slider(damage, 1, 100);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        ability = (AbilityEnum)EditorGUILayout.EnumPopup("Combat type :", ability);
        GUILayout.EndHorizontal();
        //Button
        if (GUILayout.Button("Update"))
        {
            character.SetDamage((int)damage);
        }
    }
}
