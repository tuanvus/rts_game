using UnityEditor;
using UnityEngine;

public class CharacterDataInspector : Editor
{
    string characterName = "Name";
    int health;
    public override void OnInspectorGUI()
    {
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperLeft;
        style.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Character Data", style, GUILayout.ExpandWidth(true));
        CharacterData characterData = (CharacterData)target;
        
        characterName = characterData.CharacterName;
        health = characterData.characterHealth;

        GUILayout.BeginHorizontal();
        characterData.CharacterName =
        EditorGUILayout.TextField("Character's Name", characterName);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        characterData.characterHealth =
        EditorGUILayout.IntField("Health", health);
        GUILayout.EndHorizontal();

    }
}
