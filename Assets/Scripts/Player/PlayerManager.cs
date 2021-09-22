using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.InputManager;
namespace RTS.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Transform playerUnits;
        public Transform enemyUnits;
        public Transform playerBuilding;
        void Start()
        {
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuilding);


        }

        void Update()
        {
            InputHandler.Instance.HandleUnitMovement();
        }
        public void SetBasicStats(Transform type)
        {
            Transform pUnits = PlayerManager.Instance.playerUnits;
            Transform eUnits = PlayerManager.Instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyUnits eU = tf.GetComponent<Units.Enemy.EnemyUnits>();
                        eU.baseStats = Units.UnitHandler.Instance.GetBasicUnitStats(name);

                    }
                    //set unit stats in each units
                    else if(type == playerBuilding)
                    {
                        Buildings.PlayerBuilding pB = tf.GetComponent<Buildings.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.Instance.GetBasicBuildingStats(name);
                    }

                    //if we have any upgraes to unit stats




                }
            }
        }
    }
}