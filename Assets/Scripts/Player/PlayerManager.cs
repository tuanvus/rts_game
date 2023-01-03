using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.InputManager;
namespace RTS.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Transform playerUnits;
        public Transform playerBuilding;
        public List<PeasantUnit> List_Peasants;
        public ResourceManager resourceManager;
        void Start()
        {
            //SetBasicStats(playerUnits);
            //SetBasicStats(enemyUnits);
            // SetBasicStats(playerBuilding);


        }
        public void Initialize(ResourceManager rm)
        {
            resourceManager = rm;

            foreach (var peasantUnit in List_Peasants)
            {
                peasantUnit.OnUpdateResource += resourceManager.OnUpdateResource;
            }

        }

        void Update()
        {
            InputHandler.Instance.HandleUnitMovement();
        }
        public void SetBasicStats(Transform type)
        {
            Transform pUnits = PlayerManager.Instance.playerUnits;

            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    // var stats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                    // if (type == playerUnits)
                    // {
                    //     Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                    //     pU.baseStats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                    // }
                    // //set unit stats in each units
                    // else if (type == playerBuilding)
                    // {
                    //     Buildings.PlayerBuilding pB = tf.GetComponent<Buildings.PlayerBuilding>();
                    //     pB.baseStats = Buildings.BuildingHandler.Instance.GetBasicBuildingStats(name);
                    // }

                    //if we have any upgraes to unit stats




                }
            }
        }
    }
}