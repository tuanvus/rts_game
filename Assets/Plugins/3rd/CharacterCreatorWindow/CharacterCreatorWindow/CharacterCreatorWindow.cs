using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterCreatorWindow : EditorWindow
{
    [MenuItem("Ntk/CharacterCreator")]
    public static void ShowWindow()
    {
        GetWindow<CharacterCreatorWindow>(false, "Character Creator", true);
    }

    GameObject gameObject;
    Editor gameObjectEditor;

    AbilityEnum ability;

    Animator animator;

    float damage = 25;
    string characterName = "Nama";
    int characterHealth = 100;
    bool rootMotion;

    Vector2 scroll;
    private void OnGUI()
    {
        scroll =
        EditorGUILayout.BeginScrollView(scroll);
        characterName =
        EditorGUILayout.TextField("Character's Name", characterName);
        characterHealth =
        EditorGUILayout.IntField("Player Health", characterHealth);

        GUILayout.Label("Animator");
        EditorGUILayout.BeginHorizontal();
        animator = (Animator)EditorGUILayout.ObjectField(animator, typeof(Animator), true);

        rootMotion =
        EditorGUILayout.Toggle("Has Root Motion", rootMotion);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Character Prefab/Gameobject");
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage");
        damage = EditorGUILayout.Slider(damage, 1, 100);
        EditorGUILayout.EndHorizontal();

        ability = (AbilityEnum)EditorGUILayout.EnumPopup("Combat type :", ability);

        if (gameObject != null)
        {
            if (gameObjectEditor == null)
                gameObjectEditor = Editor.CreateEditor(gameObject);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200, 300), EditorStyles.whiteLabel);
        }

        GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("Update"))
        {
            gameObjectEditor = null;
            Repaint();

        }

        EditorGUILayout.EndScrollView();

        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button("Docs"))
        {
            Application.OpenURL("URLTOOPEN");
        }

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Reset"))
        {
            rootMotion = false;
            characterHealth = 100;
            characterName = "Nama";
            damage = 25;
            ability = AbilityEnum.mage;
            animator = null;
            
            Repaint();
        }

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Create Character"))
        {
            CreateCharacter();
        }
        //EditorGUILayout.EndScrollView();
    }

    void CreateCharacter()
    {
        if (gameObjectEditor == null)
            return;

        GameObject currentObject = (GameObject)Instantiate(gameObjectEditor.target);

        currentObject.AddComponent<Ability>().abilityType = ability;
        currentObject.GetComponent<Ability>().SetDamage((int)damage);

        currentObject.AddComponent<Animator>();
        Animator anim = currentObject.GetComponent<Animator>();
        anim = animator;
        if(anim != null)
            anim.applyRootMotion = rootMotion;

        currentObject.AddComponent<CharacterData>().CharacterName = characterName;
        currentObject.GetComponent<CharacterData>().characterHealth = characterHealth;
    }
}
