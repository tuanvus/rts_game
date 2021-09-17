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
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
            pUnitLayer = LayerMask.NameToLayer("PlayerUnits");

        }

        public (float cost,float aggroRange, float attack, float atkRange, float health, float armor) GetBasicUnitStats(string type)
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
                    return (0,0, 0, 0, 0, 0);
            }
            return (unit.baseStats.cost, unit.baseStats.aggroRange, unit.baseStats.attack, unit.baseStats.atkRange, unit.baseStats.health, unit.baseStats.armor);
        }
        public void SetBasicUnitStats(Transform type)
        {
            Transform pUnits = PlayerManager.Instance.playerUnits;
            Transform eUnits = PlayerManager.Instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach(Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length-1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    if (type == pUnits)
                    {
                        Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();
                        pU.baseStats.cost = stats.cost;
                        pU.baseStats.aggroRange = stats.aggroRange;

                        pU.baseStats.attack = stats.attack;
                        pU.baseStats.atkRange = stats.atkRange;
                        pU.baseStats.health = stats.health;
                        pU.baseStats.armor = stats.armor;
                    }
                    else if(type == eUnits)
                    {
                        Enemy.EnemyUnits eU = unit.GetComponent<Enemy.EnemyUnits>();
                        eU.baseStats.cost = stats.cost;
                        eU.baseStats.aggroRange = stats.aggroRange;

                        eU.baseStats.attack = stats.attack;
                        eU.baseStats.atkRange = stats.atkRange;
                        eU.baseStats.health = stats.health;
                        eU.baseStats.armor = stats.armor;
                    }
                    //set unit stats in each units
                 

                    //if we have any upgraes to unit stats


                }
            }
        }
    }
}