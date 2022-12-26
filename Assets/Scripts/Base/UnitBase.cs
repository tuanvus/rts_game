using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTS
{
    public enum TypeUnit
    {
        Archer,//xa thu
        Commander,//Chỉ huy
        Crossbowman,//xa thu nỏ
        Halberdier, //giáo
        Knight,//hiệp sĩ
        Mage,//pháp sư

        HeavyCavalry, //kị binh hạng nặng
        HeavyInfantry,//bộ binh hạng nặng
        HeavySwordman,//kiếm sĩ hạng nặng

        HighPriest,//thầy tế thượng phẩm

        LightCavalry,//kị binh hạng nhẹ
        LightInfantry,// bộ binh hạng nhẹ


        King,//vua
        MountedKing,// vua cưỡi ngựa
        MountedKnight,//kị sĩ cưỡi ngựa
        MountedMage,// phù thủy cưỡi ngựa
        MountedPaladin,//vua cưỡi ngựa nâng cấp
        MountedPriest,//thầy tế cưỡi ngựa
        MountedScout,//cung thủ cưỡi ngựa
        Paladin,//vua nâng cấp
        Peasant,//nông dân 
        Priest,//thầy tế 
        Scout,//xa thu nâng cấp 
        Settler,//xe chở hàng
        Spearman,// bộ binh giáo
        Swordman // bộ binh
    }
    public enum StateUnit
    {
        Idle,
        Move,
        Attack,
        Dead,
        Patroll,
        Hit,


    }

    public class UnitBase : MonoBehaviour
    {
        [Header("base")]
        [SerializeField] protected AnimatorHandle animatorHandle;
        [SerializeField] protected TypeUnit typeUnit;
        public StatInfoUnit statInfoUnit;
        public StateUnit state;

        public GameObject unitsStatsDisplay;

        public Image healthBarAmount;
        public NavMeshAgent navAgent;
        public UnitBase target;
        public bool canAtk = false;
        public bool isAlive = true;
        public float distance;
        public LayerMask layerTarget;
        void Start()
        {

        }
        public virtual void Initialized()
        {
            animatorHandle.Initialized();
            canAtk = false;
            unitsStatsDisplay.SetActive(false);
            target = null;
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
                    target = rangColliders[i].GetComponent<UnitBase>();
                    //aggroUnit = aggroTaget.gameObject.GetComponent<Player.PlayerUnit>();
                    Debug.Log("target");
                    break;
                }
            }
        }
        protected void MoveToTarget()
        {
            distance = Vector3.Distance(target.transform.position, transform.position);
            navAgent.stoppingDistance = (statInfoUnit.atkRange + 1);
        }
        protected void AttackUnit()
        {
            if (canAtk && distance <= statInfoUnit.atkRange)
            {
                target.TakeDamage(statInfoUnit.damage);
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