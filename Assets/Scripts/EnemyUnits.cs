using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace RTS.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnits : MonoBehaviour
    {
        public UnitStatTypes.Base baseStats;

        private NavMeshAgent navAgent;
        private Collider[] rangColliders;
        private Transform aggroTaget;
        private bool hasAggro = false;
        private float distance;

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();

        }
        private void Update()
        {
            if(!hasAggro)
            {
                CheckForEnemyTarget();
            }    
            else
            {
                MoveToAggroTarget();
                Debug.Log("vao");
            }    
        }
        private void CheckForEnemyTarget()
        {
            rangColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);
            Debug.Log($"count {rangColliders.Length}");

            for (int i = 0; i < rangColliders.Length; i++)
            {
                if (rangColliders[i].gameObject.layer == UnitHandler.Instance.pUnitLayer)
                {
                    aggroTaget = rangColliders[i].gameObject.transform;
                    hasAggro = true;
                    Debug.Log("vao k");
                    break;
                }
            }
        }
        private void MoveToAggroTarget()
        {
            distance = Vector3.Distance(aggroTaget.position, transform.position);
           // navAgent.stoppingDistance = (baseStats.atkRange + 1);
            if (distance <= baseStats.aggroRange)
            {
                Debug.Log($"fa {aggroTaget.position}");
                navAgent.SetDestination(aggroTaget.position);
              //  navAgent.SetDestination(Vector3.zero);

            }
        }
        private void OnEnable()
        {
        }





    }
}