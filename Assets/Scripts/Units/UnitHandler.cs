using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;
namespace RTS.Units
{
    public class UnitHandler : Singleton<UnitHandler>
    {
        public LayerMask pUnitLayer;
        public LayerMask eUnitLayer;



        [SerializeField]
        BasicUnit worker;

        [SerializeField]
        BasicUnit warrior;

        [SerializeField]
        BasicUnit healer;

        private void Start()
        {
            // eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
            // pUnitLayer = LayerMask.NameToLayer("PlayerUnits");

        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "healer":
                    unit = healer;
                    break;
                default:
                    Debug.Log($"Unity :{type} could not be found ");
                    return null;
            }
            return unit.baseStats;
        }
       
    }
}