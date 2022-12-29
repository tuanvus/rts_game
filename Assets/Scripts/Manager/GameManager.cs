using System.Collections;
using System.Collections.Generic;
using RTS.Player;
using UnityEngine;
namespace RTS
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerManager playerManager;
        public ResourceManager resourceManager;

        void Start()
        {
            playerManager.Initialize(resourceManager);
            resourceManager.Initialize();
        }

        void Update()
        {

        }
    }
}