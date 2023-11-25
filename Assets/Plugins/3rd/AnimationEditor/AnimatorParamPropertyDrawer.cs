#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimatorParamAttribute))]
public class AnimatorParamPropertyDrawer : PropertyDrawer
{
    private RuntimeAnimatorController _animatorRuntimeController;
    private Animator animator;
    private AnimatorController _animatorController;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_animatorRuntimeController == null)
        {
            var animAttribute = attribute as AnimatorParamAttribute;
            if (string.IsNullOrEmpty(animAttribute.animatorPath))
            {
                Component c = property.serializedObject.targetObject as Component;
                if (c != null)
                {
                    var animatorComponent = c.GetComponent<Animator>();
                    if (animatorComponent != null)
                    {
                        _animatorRuntimeController = animatorComponent.runtimeAnimatorController;
                        animator = animatorComponent;
                    }
                    else
                    {
                        var childAnimator = c.GetComponentInChildren<Animator>();
                        if (childAnimator != null)
                        {
                            animator = childAnimator;
                            _animatorRuntimeController = childAnimator.runtimeAnimatorController;
                        }
                    }
                }
            }
            else
            {
                _animatorRuntimeController = GetAssetAtPath<RuntimeAnimatorController>(animAttribute.animatorPath);
            }
        }

        if (_animatorRuntimeController == null)
        {
            GUI.Box(position, "Not found animator");
        }
        else
        {
            EditorGUI.BeginProperty(position, label, property);
            float labelWidth = 120;
            if (position.width * 0.425f > labelWidth)
            {
                labelWidth = position.width * 0.425f;
            }

            var rect1 = new Rect(position.x, position.y, labelWidth, position.height);
            var rect2 = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, position.height);
            GUI.Label(rect1, label);
            if (GUI.Button(rect2, property.stringValue, EditorStyles.popup))
            {
                Selector(property);
            }

            EditorGUI.EndProperty();
        }
    }

    public static T GetAssetAtPath<T>(string path) where T : Object
    {
#if UNITY_EDITOR
        return AssetDatabase.LoadAssetAtPath<T>(path);
#else
        return default;
#endif
    }

    static GUIContent tempContent;

    static GUIContent TempContent(string text, Texture2D image = null, string tooltip = null)
    {
        if (tempContent == null)
        {
            tempContent = new GUIContent();
        }

        tempContent.text = text;
        tempContent.image = image;
        tempContent.tooltip = tooltip;
        return tempContent;
    }

    void Selector(SerializedProperty property)
    {
        GenericMenu menu = new GenericMenu();

        if (animator != null)
        {
            _animatorController = animator.runtimeAnimatorController as AnimatorController;
            // Lấy ra danh sách các layers của AnimatorController
            AnimatorControllerLayer[] layers = _animatorController.layers;

            AnimatorControllerParameter[] parameters = _animatorController.parameters;

            foreach (var parameter in parameters)
            {
                string name = parameter.name;
                menu.AddItem(new GUIContent(name), !property.hasMultipleDifferentValues && name == property.stringValue,
                    HandleSelect, new SpineDrawerValuePair(name, property));
            }
        }
        menu.ShowAsContext();
    }
    static void HandleSelect(object val)
    {
        var pair = (SpineDrawerValuePair)val;
        pair.property.stringValue = pair.stringValue;
        pair.property.serializedObject.ApplyModifiedProperties();
    }
}
#endif