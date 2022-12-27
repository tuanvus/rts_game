using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace RTS
{
    public class PeasantUnit : UnitBase
    {


        [Header("PeasantUnit")]
        [FoldoutGroup("Peasan")] public Transform nodeResources;
        [FoldoutGroup("Peasan")] public Transform nodeStorage;
        [FoldoutGroup("Peasan")] public int capacityResources;
        [FoldoutGroup("Peasan")] public List<Transform> resourcesType;
        [FoldoutGroup("Peasan")] public List<GameObject> listResources;

        [FoldoutGroup("Peasan")] public float rangeDistance;



        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
        }
        void StateHandle(StateUnit stateUnit)
        {
            state = stateUnit;
            switch (stateUnit)
            {
                case StateUnit.Idle:
                    break;
                case StateUnit.Move:
                    break;
                case StateUnit.Attack:
                    break;
                case StateUnit.Dead:
                    break;
                case StateUnit.Hit:
                    break;
                case StateUnit.MovingToResour:
                    MovingToResources();
                    break;
                case StateUnit.GatheringResources:
                    GatheringResources();
                    break;
                case StateUnit.MovingToStorage:
                    MovingToStorage();
                    break;
                case StateUnit.FindNodeResources:
                    FindNodeResources();
                    break;
                default:
                    break;
            }
        }
        void MovingToResources()
        {
            navAgent.SetDestination(nodeResources.position);
            navAgent.stoppingDistance = rangeDistance;
        }
        void MovingToStorage()
        {
            navAgent.SetDestination(nodeStorage.position);
            navAgent.stoppingDistance = rangeDistance;
        }
        void FindNodeResources()
        {
        }
        void GatheringResources()
        {
        }
    }
}