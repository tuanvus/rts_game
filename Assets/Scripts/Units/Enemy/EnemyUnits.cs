using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace RTS.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnits : MonoBehaviour
    {
        
        public StatInfoUnit baseStats;

        public GameObject unitsStatsDisplay;

        public Image healthBarAmount;

        public float currenHealth;

        private NavMeshAgent navAgent;

        [SerializeField] Collider[] rangColliders;

        private Transform aggroTaget;

        private bool hasAggro = false;

        private float distance;
        private float atkCooldown;
        private Player.PlayerUnit aggroUnit;

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            currenHealth = baseStats.health;
        }

        private void Update()
        {
            atkCooldown -= Time.deltaTime;



            if (!hasAggro)
            {
                CheckForEnemyTarget();
            }
            else
            {
                Attack();
                MoveToAggroTarget();
                Debug.Log("vao");
            }
        }


        private void CheckForEnemyTarget()
        {
            rangColliders =
                Physics.OverlapSphere(transform.position, baseStats.atkRange);
            Debug.Log($"count {rangColliders.Length}");

            for (int i = 0; i < rangColliders.Length; i++)
            {
                if (
                    rangColliders[i].gameObject.layer ==
                    UnitHandler.Instance.pUnitLayer
                )
                {
                    aggroTaget = rangColliders[i].gameObject.transform;
                    aggroUnit = aggroTaget.gameObject.GetComponent<Player.PlayerUnit>();
                    hasAggro = true;
                    Debug.Log("vao k");
                    break;
                }
            }
        }
        private void Attack()
        {
            if(atkCooldown <= 0 && distance <= baseStats.atkRange +1)
            {
                aggroUnit.GetComponentInChildren<UnitStatDisplay>().TakeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
            }
        }
        private void MoveToAggroTarget()
        {
            if (aggroTaget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                distance = Vector3.Distance(aggroTaget.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange + 1);
                if(distance <= baseStats.atkRange)
                {
                    navAgent.SetDestination(aggroTaget.position);
                }
            }
        }

        private void OnEnable()
        {
        }
    
    

    }
}
