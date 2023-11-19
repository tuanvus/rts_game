using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CharacterCreator : MonoBehaviour
{
    public static bool UseRootMotion
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetBool("UseRootMotion", false);
#else
            return false;
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetBool("UseRootMotion", value);
#endif
        }
    }

    public static int PlayerHealth
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt("PlayerHealth", 100);
#else
            return false;
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt("PlayerHealth", value);
#endif
        }
    }


    public static string PlayerName
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetString("PlayerName", "Nama");
#else
            return false;
#endif
        }

        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetString("PlayerName", value);
#endif
        }
    }

}
