﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveAllComponentInParent))]
public class RemoveCollider : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(20);

        var style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };
        string lb = "Remove All Component In Parent";
        GUILayout.Label(lb, style, GUILayout.ExpandWidth(true));

        var remove = (RemoveAllComponentInParent)target;
        remove.Count = 0;


        GUILayout.Space(5F);
        if (GUILayout.Button("All Components Except Models"))
        {
            remove.DestroyAllComponet();
        }
        if (GUILayout.Button("Gameobject no componets"))
        {
            remove.DestroyObjectNoComponent();
        }
        if (GUILayout.Button("Scripts"))
        {
            remove.DestroyScripts();
        }
        if (GUILayout.Button("Rigidbodies"))
        {
            remove.DestroyRigidbodys();
        }
        if (GUILayout.Button("Colliders"))
        {
            remove.DestroyColliders();
        }

        if (GUILayout.Button("AudioSources"))
        {
            remove.DestroyAudioSource();
        }
        if (GUILayout.Button("Lights"))
        {
            remove.DestroyLight();
        }
        if (GUILayout.Button("Animations"))
        {
            remove.DestroyAnimations();
        }
        GUILayout.Space(15);


        if (GUILayout.Button("Undo"))
        {
            Debug.Log("-- Undo --");
            Undo.PerformUndo();
        }

        GUILayout.Space(10F);
    }
}
public class RemoveAllComponentInParent : MonoBehaviour
{
    [Space]
    public Transform transformParentRemove;
    public int Count {private get; set; }

    // Component delete
    [Space]
    private Collider[] colliders;
    private AudioSource[] audioSource;
    private Light[] lightPoint;
    private MonoBehaviour[] monoScript;
    private Rigidbody[] rigidbodies;
    private Animation[] animations;
    private Component[] components;
    private Component[] objNoComponent;

    // Method
    public void Start()
    {
        if(transformParentRemove == null)
        {
            transformParentRemove = this.transform;
        }
    }
    public void DestroyScripts()
    {
        monoScript = transformParentRemove.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour scripts in monoScript)
        {
            if (monoScript.GetType() != typeof(RemoveAllComponentInParent))
            {
                Debug.Log("Name: " + scripts.name);
                EditorUtility.SetDirty(scripts);
                Undo.DestroyObjectImmediate(scripts);
            }
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyAnimations()
    {
        animations = transformParentRemove.GetComponentsInChildren<Animation>();
        foreach (Animation anim in animations)
        {
            Debug.Log("Name: " + anim.name);
            EditorUtility.SetDirty(anim);
            Undo.DestroyObjectImmediate(anim);
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyRigidbodys()
    {
        rigidbodies = transformParentRemove.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigidbodies)
        {
            Debug.Log("Name: " + rigid.name);
            EditorUtility.SetDirty(rigid);
            Undo.DestroyObjectImmediate(rigid);
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyColliders()
    {
        colliders = transformParentRemove.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            Debug.Log("Name: " + collider.name);
            EditorUtility.SetDirty(collider);
            Undo.DestroyObjectImmediate(collider);
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyAudioSource()
    {
        audioSource = transformParentRemove.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource audio in audioSource)
        {
            Debug.Log("Name: " + audio.name);
            EditorUtility.SetDirty(audio);
            Undo.DestroyObjectImmediate(audio);
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyLight()
    {
        lightPoint = transformParentRemove.GetComponentsInChildren<Light>();
        foreach (Light light in lightPoint)
        {
            Debug.Log("Name: " + light.name);
            EditorUtility.SetDirty(light);
            Undo.DestroyObjectImmediate(light);
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyAllComponet()
    {
        components = transformParentRemove.GetComponentsInChildren<Component>();
        foreach (Component componet in components)
        {
            Debug.Log("Name: " + componet.name);
            if (componet.GetType() != typeof(MeshFilter)
            && componet.GetType() != typeof(MeshRenderer)
            && componet.GetType() != typeof(RemoveAllComponentInParent))
            {
                EditorUtility.SetDirty(componet);
                Undo.DestroyObjectImmediate(componet);
            }
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
    public void DestroyObjectNoComponent()
    {
        objNoComponent = transformParentRemove.GetComponentsInChildren<Transform>();
        foreach (Component trans in objNoComponent)
        {
            Debug.Log("Name: " + trans.name);
            var t = trans.GetComponents<Component>().Length;
            if (t <= 1)
            {
                EditorUtility.SetDirty(trans.gameObject);
                Undo.DestroyObjectImmediate(trans.gameObject);
            }
            Count++;
        }
        Debug.Log("Count: " + Count);
    }
}
public static class HasComponentInObject
{
    public static bool HasComponent<T>(this GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }
}
#endif