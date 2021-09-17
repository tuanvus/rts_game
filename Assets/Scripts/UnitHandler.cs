using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Units
{
    public class UnitHandler : Singleton<UnitHandler>
    {
        [SerializeField]
        BasicUnit worker;

        [SerializeField]
        BasicUnit warrior;

        [SerializeField]
        BasicUnit healer;

        public (int cost, int attack, int atkRange, int health, int armor) GetBasicUnitStats(string type)
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
                    return (0, 0, 0, 0, 0);
            }
            return (unit.cost, unit.attack, unit.atkRange, unit.health, unit.armor);
        }
        public void SetBasicUnitStats(Transform type)
        {
            foreach(Transform child in type)
            {
                foreach(Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length-1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    Player.PlayerUnit pU;
                    Player.PlayerUnit pE;

                    if (type == RTS.Player.PlayerManager.Instance.playerUnits)
                    {
                        pU = unit.GetComponent<Player.PlayerUnit>();
                        pU.cost = stats.cost;
                        pU.attack = stats.attack;
                        pU.atkRange = stats.atkRange;
                        pU.health = stats.health;
                        pU.armor = stats.armor;
                    }
                    else if(type == RTS.Player.PlayerManager.Instance.enemyUnits)
                    {
                        //pE = unit.GetComponent<Player.PlayerUnit>();
                        //pE.cost = stats.cost;
                        //pE.attack = stats.attack;
                        //pE.atkRange = stats.atkRange;
                        //pE.health = stats.health;
                        //pE.armor = stats.armor;
                    }
                    //set unit stats in each units
                 

                    //if we have any upgraes to unit stats


                }
            }
        }
    }
}