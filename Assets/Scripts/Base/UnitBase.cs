using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using RTS.SO;

namespace RTS
{
    public abstract class UnitBase : MonoBehaviour
    {
        [SerializeField] protected TypeUnit typeUnit;
        [SerializeField] protected int LvUnit;
        [SerializeField] protected int speed;

        [FoldoutGroup("BaseUnit")][SerializeField] protected AnimatorHandle animatorHandle;
        [FoldoutGroup("BaseUnit")] public StatInfoUnit statInfoUnit;
        [FoldoutGroup("BaseUnit")] public StateUnit state;
        [FoldoutGroup("BaseUnit")] public GameObject unitsStatsDisplay;
        [FoldoutGroup("BaseUnit")] public Image healthBarAmount;
        [FoldoutGroup("BaseUnit")] public NavMeshAgent navAgent;
        [FoldoutGroup("BaseUnit")] public UnitBase targetUnit;
        [FoldoutGroup("BaseUnit")] public bool canAtk = false;
        [FoldoutGroup("BaseUnit")] public bool isAlive = true;
        [FoldoutGroup("BaseUnit")] public float distance;
        [FoldoutGroup("BaseUnit")] public LayerMask layerTarget;

        // protected void Reset()
        // {
        //     Debug.Log("Reset");
        //     LoadComponents();
        // }
        protected void Reset()
        {
            Debug.Log("Reset Data");
            LoadComponents();
            ResetValue();
        }
        protected virtual void ResetValue()
        {
            LvUnit = 1;
            speed = 5;
            state = StateUnit.Idle;
            canAtk = false;
            isAlive = true;
        }
        protected virtual void LoadComponents()
        {
            animatorHandle = transform.GetComponentInChildren<AnimatorHandle>();
            string path = "DataSO/Peasant";
            statInfoUnit.SetValue(Resources.Load<BasicUnit>(path));
            navAgent = transform.GetComponent<NavMeshAgent>();
            unitsStatsDisplay = transform.Find("UnitsStatsDisplay").gameObject;
            healthBarAmount = unitsStatsDisplay.transform.Find("HealthBar").GetComponent<Image>();
        }

        void Start()
        {
            Initialized();
        }
        public virtual void Initialized()
        {
            animatorHandle.Initialized();
            canAtk = false;
            unitsStatsDisplay.SetActive(false);
            targetUnit = null;
            isAlive = true;
            state = StateUnit.Idle;
            navAgent.speed = speed;
        }
        void Update()
        {

        }
        protected void FindTarget()
        {
            var rangColliders =
                 Physics.OverlapSphere(transform.position, statInfoUnit.atkRange);
            Debug.Log($"count {rangColliders.Length}");

            for (int i = 0; i < rangColliders.Length; i++)
            {
                if (rangColliders[i].gameObject.layer == layerTarget)

                {
                    targetUnit = rangColliders[i].GetComponent<UnitBase>();
                    //aggroUnit = aggroTaget.gameObject.GetComponent<Player.PlayerUnit>();
                    Debug.Log("target");
                    break;
                }
            }
        }

        protected void AttackUnit()
        {
            if (canAtk && distance <= statInfoUnit.atkRange)
            {
                targetUnit.TakeDamage(statInfoUnit.damage);
            }
        }
        protected virtual void MoveUnit(Transform _destination, float _stoppingDistance = 0)
        {
            navAgent.SetDestination(_destination.position);
            navAgent.stoppingDistance = _stoppingDistance;
            state = StateUnit.Move;
        }
        public int CalculateDamage(int damage, int armor)
        {
            // Tính tỉ lệ chí mạng dựa trên giáp
            float armorReduction = armor / (armor + 100.0f);

            // Giảm damage tương ứng với tỉ lệ chí mạng
            float reducedDamage = damage * (1.0f - armorReduction);

            return (int)reducedDamage;
        }
        protected void TakeDamage(int damage)
        {
            statInfoUnit.health -= CalculateDamage(damage, statInfoUnit.armor);

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, statInfoUnit.atkRange);
        }

        public virtual void SetTarget<T>(T target) where T : Component
        {
            if (target != null)
            {
                Debug.Log("target is not null");
                GetTypeTarget(target);
            }
            else
            {
                Debug.LogError("target is null");
            }
        }
        protected abstract void GetTypeTarget<T>(T target);

    }

    [System.Serializable]
    public class StatInfoUnit
    {
        //public int health;
        //public int healthMax;
        //public int timeDelayAtk;
        //public int rangeAtk;
        //public int damageAtk;
        //public int armor;
        public StatInfoUnit()
        {

        }
        public void SetValue(BasicUnit basicUnit)
        {
            cost = basicUnit.cost;
            atkRange = basicUnit.atkRange;
            atkSpeed = basicUnit.atkSpeed;
            damage = basicUnit.damage;
            health = basicUnit.health;
            healthMax = basicUnit.healthMax;
            armor = basicUnit.armor;
            critRatio = basicUnit.critRatio;
        }

        public float cost, atkRange, atkSpeed;
        public int armor;
        public int damage;
        public int health;
        public int healthMax;
        public int critRatio;


    }
}