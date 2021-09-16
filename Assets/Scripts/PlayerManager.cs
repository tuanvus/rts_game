using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.InputManager;
namespace RTS.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public  Transform playerUnits;
        void Start()
        {

        }

        void Update()
        {
            InputHandler.Instance.HandleUnitMovement();
        }
    }
}