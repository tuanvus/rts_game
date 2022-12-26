using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS
{
    public class StateUnitAnimation
    {
        public static string Idle = "Idle";
        public static string Run = "Run";
        public static string Walk = "Walk";
        public static string Attack = "Attack";
        public static string Die = "Die";

    }
    public class AnimatorHandle : MonoBehaviour
    {
        public Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Initialized()
        {
            animator.Rebind();
            animator.Play(StateUnitAnimation.Idle);
        }
        void Start()
        {

        }


    }
}