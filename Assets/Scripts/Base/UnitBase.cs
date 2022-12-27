using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Sirenix.OdinInspector;
namespace RTS
{
    public class UnitBase : MonoBehaviour
    {
        [SerializeField] protected TypeUnit typeUnit;
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
        void Start()
        {

        }
        public virtual void Initialized()
        {
            animatorHandle.Initialized();
            canAtk = false;
            unitsStatsDisplay.SetActive(false);
            targetUnit = null;
            isAlive = true;
            state = StateUnit.Idle;
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
        protected void MoveToTarget(Vector3 targetPos)
        {
            distance = Vector3.Distance(targetPos, transform.position);
            navAgent.stoppingDistance = (statInfoUnit.atkRange + 1);
        }
        protected void AttackUnit()
        {
            if (canAtk && distance <= statInfoUnit.atkRange)
            {
                targetUnit.TakeDamage(statInfoUnit.damage);
            }
        }
        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
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
        public float cost, atkRange, atkSpeed;
        public int armor;
        public int damage;
        public int health;
        public int healthMax;
        public int critRatio;



    }
}