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

        public bool isPlayerUnit;
        public unitType type;
        public new string unitName;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;


        public int cost;
        public int attack;
        public int health;
        public int armor;

    }
}
