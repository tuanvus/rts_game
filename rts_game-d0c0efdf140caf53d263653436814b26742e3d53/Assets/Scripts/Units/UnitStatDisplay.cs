using UnityEngine;
using UnityEngine.UI;
using System;
namespace RTS.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth;
        public float armor;
        public float currenHealth;
        [SerializeField] private Image healthBarAmount;
        private bool isPlayerUnit = false;

        void Start()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                armor = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armor;
                isPlayerUnit = true;
            }
            catch (Exception)
            {
                Debug.Log($"no player Unit :{gameObject.GetComponentInParent<GameObject>().name}");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnits>().baseStats.health;
                    armor = gameObject.GetComponentInParent<Enemy.EnemyUnits>().baseStats.armor;
                    isPlayerUnit = false;
                }
                catch (System.Exception)
                {
                    Debug.Log($"no player Unit :{gameObject.GetComponentInParent<GameObject>().name}");
                }
            }




        }
        void Update()
        {


            HandleHealthChanged();
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armor;
            currenHealth -= totalDamage;

        }
        private void HandleHealthChanged()
        {
            Camera cam = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
            healthBarAmount.fillAmount = currenHealth / maxHealth;
            if (currenHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.Instance.selectUnits.Remove(gameObject.transform);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);

            }
        }
    }
}