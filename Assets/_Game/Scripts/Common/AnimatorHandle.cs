using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class StateUnitAnimation
{
    public static string Idle = "Idle";
    public static string Run = "Run";
    public static string Walk = "Walk";
    public static string Attack = "Attack";
    public static string Die = "Die";
    public static string Work_Wood = "Wood";
    public static string Work_Food = "Food";
    public static string Work_Gold = "Gold";
    public static string Speed = "Speed";
    public static string Idle_State = "Idle_State";
    public static string Run_State = "Run_State";
}

public class AnimatorHandle : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnValidate()
    {
         TryGetComponent(out animator);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialized()
    {
        animator.Play(StateUnitAnimation.Idle);
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
        Debug.Log("nameAnimation = " + nameAnimation + " value = " + value);
        animator.SetFloat(nameAnimation, value);
    }

    public void SetBoolAnimation(string nameAnimation, bool value)
    {
        animator.SetBool(nameAnimation, value);
    }
}