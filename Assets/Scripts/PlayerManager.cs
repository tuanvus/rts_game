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

        void Start()
        {
            Units.UnitHandler.Instance.SetBasicUnitStats(playerUnits);
            Units.UnitHandler.Instance.SetBasicUnitStats(enemyUnits);

        }

        void Update()
        {
            InputHandler.Instance.HandleUnitMovement();
        }
    }
}