using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AnimatorEventAttribute : DrawerAttribute
    {
        public static Dictionary<string,AnimationClip> EventData = new Dictionary<string, AnimationClip>();
        public string AnimatorName { get; private set; }
        public AnimatorControllerParameterType? AnimatorParamType { get; private set; }

        public AnimatorEventAttribute(string animatorName)
        {
            AnimatorName = animatorName;
            AnimatorParamType = null;
        }

        public AnimatorEventAttribute(string animatorName, AnimatorControllerParameterType animatorParamType)
        {
            AnimatorName = animatorName;
            AnimatorParamType = animatorParamType;
        }

        public AnimatorEventAttribute()
        {
        }
        public static void AddAnimationEvent(string evenName, float time, string functionName)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.functionName = functionName;
            animationEvent.time = time;
            EventData[evenName].AddEvent(animationEvent);
        }
    }
}
