
using UnityEngine;

namespace RTS.Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
       public class Base
        {
            public float cost, atkRange,atkSpeed, attack, health, armor;
        }

    }
}