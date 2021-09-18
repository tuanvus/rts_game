using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTS.Units.Player
{
    [RequireComponent(typeof (NavMeshAgent))]
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
        private void Update()
        {
            HandleHealthChanged();
        }
        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination (_destination);
        }
        private void HandleHealthChanged()
        {
            Camera cam = Camera.main;
            unitsStatsDisplay.transform.LookAt (unitsStatsDisplay.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
            healthBarAmount.fillAmount = currenHealth / baseStats.health;
            if (currenHealth <= 0)
            {
                Die();
            }
        }
        public void TakeDamage(float damage)
        {
            float totalDamage = damage - baseStats.armor;
            currenHealth -= totalDamage;

        }
        private void Die()
        {
            InputManager.InputHandler.Instance.selectUnits.Remove(gameObject.transform);

            Destroy(gameObject);
        }
        
    }
}
