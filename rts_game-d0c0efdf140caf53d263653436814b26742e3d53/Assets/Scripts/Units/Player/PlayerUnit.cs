using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTS.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        public UnitStatTypes.Base baseStats;

        public GameObject unitsStatsDisplay;

        public Image healthBarAmount;

        public float currenHealth;

        private NavMeshAgent navAgent;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }

    }
}
