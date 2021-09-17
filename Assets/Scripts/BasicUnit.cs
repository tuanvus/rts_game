using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS.Units
{


    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        }
        [Space(15)]
        [Header (" Unit Setting")]
        public bool isPlayerUnit;
        public unitType type;
        public new string unitName;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]
        public int cost;
        public int attack;
        public int atkRange;
        public int health;
        public int armor;

    }
}
