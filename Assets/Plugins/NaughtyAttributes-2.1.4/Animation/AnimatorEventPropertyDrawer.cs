using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorEventAttribute))]
    public class AnimatorEventPropertyDrawer : PropertyDrawerBase
    {
        private const string InvalidAnimatorControllerWarningMessage = "Target animator controller is null";
        private const string InvalidTypeWarningMessage = "{0} must be an int or a string";

        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            AnimatorEventAttribute animatorParamAttribute =
                PropertyUtility.GetAttribute<AnimatorEventAttribute>(property);
            bool validAnimatorController = GetAnimatorController(property, animatorParamAttribute.AnimatorName) != null;
            bool validPropertyType = property.propertyType == SerializedPropertyType.Integer ||
                                     property.propertyType == SerializedPropertyType.String;

            //return (validAnimatorController && validPropertyType)
            //    ? GetPropertyHeight(property)
            //    : GetPropertyHeight(property) + GetHelpBoxHeight();

            return GetPropertyHeight(property);
        }

        Animator FindAnimator(Object targetObject)
        {
            GameObject parentObject = null;
            if (targetObject is GameObject)
            {
                parentObject = (GameObject)targetObject;
            }
            else if (targetObject is Component)
            {
                Component component = (Component)targetObject;
                parentObject = component.gameObject;
            }

            if (parentObject != null)
            {
                //Debug.Log("parentObject = " + parentObject.transform.root);
                Transform root = parentObject.transform.root;
                Animator[] childAnimators = root.GetComponentsInChildren<Animator>();
                foreach (Animator childAnimator in childAnimators)
                {
                    return childAnimator;
                }
            }

            return null;
        }

        public List<AnimationEvent> GetEvent(Animator animator)
        {
            List<AnimationEvent> animationEvents = new List<AnimationEvent>();
            if (animator != null)
            {
                AnimatorOverrideController animatorOverrideController =
                    animator.runtimeAnimatorController as AnimatorOverrideController;
                AnimatorController animatorController = animatorOverrideController != null
                    ? animatorOverrideController.runtimeAnimatorController as AnimatorController
                    : animator.runtimeAnimatorController as AnimatorController;

                if (animatorController != null)
                {
                    Debug.Log("vaoo");

                    // Lặp qua các layer của AnimatorController
                    foreach (AnimatorControllerLayer layer in animatorController.layers)
                    {
                        // Lặp qua mọi state trong layer
                        foreach (ChildAnimatorState state in layer.stateMachine.states)
                        {
                            // Lấy clip của state
                            AnimationClip clip = state.state.motion as AnimationClip;

                            // Kiểm tra xem clip có tồn tại không
                            if (clip != null)
                            {
                                AnimationEvent[] events = clip.events;

                                // Lặp qua tất cả các sự kiện trong clip
                                foreach (AnimationEvent animEvent in events)
                                {
                                    // Ở đây bạn có thể truy cập thông tin về sự kiện như animEvent.functionName, animEvent.floatParameter, animEvent.intParameter, animEvent.stringParameter, và animEvent.objectReferenceParameter
                                    animationEvents.Add(animEvent);
                                    if (AnimatorEventAttribute.EventData.ContainsKey(animEvent.functionName))
                                    {
                                        AnimatorEventAttribute.EventData.Add(animEvent.functionName,clip);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return animationEvents;
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);
            //Debug.Log("property =" + property.serializedObject.targetObject);
            UnityEngine.Object targetObject = property.serializedObject.targetObject;
            Animator animator = FindAnimator(targetObject);
            AnimatorController animatorController1 = animator.runtimeAnimatorController as AnimatorController;
            AnimatorEventAttribute animatorParamAttribute =
                PropertyUtility.GetAttribute<AnimatorEventAttribute>(property);
            var listEvent = GetEvent(animator);

            AnimatorOverrideController animatorOverrideController =
                animator.runtimeAnimatorController as AnimatorOverrideController;
            AnimatorController animatorController = animatorOverrideController != null
                ? animatorOverrideController.runtimeAnimatorController as AnimatorController
                : animator.runtimeAnimatorController as AnimatorController;
            if (animatorController == null)
            {
                DrawDefaultPropertyAndHelpBox(rect, property, InvalidAnimatorControllerWarningMessage,
                    MessageType.Warning);
                return;
            }

            int parametersCount = listEvent.Count;
            List<string> animatorParameters = new List<string>(parametersCount);
            for (int i = 0; i < parametersCount; i++)
            {
                string parameter = listEvent[i].functionName;
                if (!animatorParameters.Contains(parameter))
                    animatorParameters.Add(parameter);
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    DrawPropertyForString(rect, property, label, animatorParameters);
                    break;
                default:
                    DrawDefaultPropertyAndHelpBox(rect, property,
                        string.Format(InvalidTypeWarningMessage, property.name), MessageType.Warning);
                    break;
            }

            EditorGUI.EndProperty();
        }


        private static void DrawPropertyForString(Rect rect, SerializedProperty property, GUIContent label,
            List<string> animatorParameters)
        {
            string paramName = property.stringValue;
            int index = 0;

            for (int i = 0; i < animatorParameters.Count; i++)
            {
                if (paramName.Equals(animatorParameters[i], System.StringComparison.Ordinal))
                {
                    index = i + 1; // +1 because the first option is reserved for (None)
                    break;
                }
            }

            string[] displayOptions = GetDisplayOptions(animatorParameters);

            int newIndex = EditorGUI.Popup(rect, label.text, index, displayOptions);
            string newValue = newIndex == 0 ? null : animatorParameters[newIndex - 1];

            if (!property.stringValue.Equals(newValue, System.StringComparison.Ordinal))
            {
                property.stringValue = newValue;
            }
        }

        private static string[] GetDisplayOptions(List<string> animatorParams)
        {
            string[] displayOptions = new string[animatorParams.Count + 1];
            displayOptions[0] = "(None)";

            for (int i = 0; i < animatorParams.Count; i++)
            {
                displayOptions[i + 1] = animatorParams[i];
            }

            return displayOptions;
        }

        private static AnimatorController GetAnimatorController(SerializedProperty property, string animatorName)
        {
            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            FieldInfo animatorFieldInfo = ReflectionUtility.GetField(target, animatorName);
            if (animatorFieldInfo != null &&
                animatorFieldInfo.FieldType == typeof(Animator))
            {
                Animator animator = animatorFieldInfo.GetValue(target) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            PropertyInfo animatorPropertyInfo = ReflectionUtility.GetProperty(target, animatorName);
            if (animatorPropertyInfo != null &&
                animatorPropertyInfo.PropertyType == typeof(Animator))
            {
                Animator animator = animatorPropertyInfo.GetValue(target) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            MethodInfo animatorGetterMethodInfo = ReflectionUtility.GetMethod(target, animatorName);
            if (animatorGetterMethodInfo != null &&
                animatorGetterMethodInfo.ReturnType == typeof(Animator) &&
                animatorGetterMethodInfo.GetParameters().Length == 0)
            {
                Animator animator = animatorGetterMethodInfo.Invoke(target, null) as Animator;
                if (animator != null)
                {
                    AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
                    return animatorController;
                }
            }

            return null;
        }
    }
}