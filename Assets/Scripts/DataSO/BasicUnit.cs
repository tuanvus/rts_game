using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS.SO
{

    [CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Basic Unit", order = 1)]
    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        }
        [Space(15)]
        [Header(" Unit Setting")]
        public bool isPlayerUnit;
        public unitType type;
        public new string unitName;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;

        [Space(15)]
        [Header("Unit Base Stats")]
        public float cost, atkRange, atkSpeed;
        public int armor;
        public int damage;
        public int health;
        public int healthMax;
        public int critRatio;

    }
}
