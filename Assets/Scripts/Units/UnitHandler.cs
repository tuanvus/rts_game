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
                        pU.baseStats = GetBasicUnitStats(unitName);
                    }
                    else if(type == eUnits)
                    {
                        Enemy.EnemyUnits eU = unit.GetComponent<Enemy.EnemyUnits>();
                        eU.baseStats = GetBasicUnitStats(unitName);

                    }
                    //set unit stats in each units


                    //if we have any upgraes to unit stats


                }
            }
        }
    }
}