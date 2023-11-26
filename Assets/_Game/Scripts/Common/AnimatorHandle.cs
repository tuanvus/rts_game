using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

[RequireComponent(typeof(AnimatorInfo))]
public class AnimatorHandle : MonoBehaviour
{
    [SerializeField] Animator animator;
    // [AnimatorParam] public string Speed;
     [SerializeField] AnimatorInfo animatorInfo;

     public AnimatorInfo Data => animatorInfo;
     
     
    private void OnValidate()
    {
        TryGetComponent(out animator);
        TryGetComponent(out animatorInfo);
        var t = GetComponent<AnimatorInfo>();
        if (t == null)
        {
            gameObject.AddComponent<AnimatorInfo>();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialized()
    {
        SetFloatAnimation(Data.p_Speed,0);
    }

    public void PlayAnimation(string nameAnimation)
    {
        animator.Play(nameAnimation);
    }

    public void PlayCrossFadeAni(string nameAnimation, float normalizedTransitionDuration, int layer,
        float normalizedTimeOffset)
    {
        animator.CrossFade(nameAnimation, normalizedTimeOffset, layer, normalizedTransitionDuration); //)
    }

    public void SetFloatAnimation(string nameAnimation, float value)
    {
      //  Debug.Log("nameAnimation = " + nameAnimation + " value = " + value);
        animator.SetFloat(nameAnimation, value);
    }

    public void SetBoolAnimation(string nameAnimation, bool value)
    {
        animator.SetBool(nameAnimation, value);
    }
    
    public void AddEvent(string nameAni,AnimationEventType eventType, Action action = null)
    {
        animator.AddEvent(nameAni,eventType, (animator) =>
        {
            action?.Invoke();
        });

    }
}