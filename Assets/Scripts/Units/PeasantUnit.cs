using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RTS.Resource;
using UnityEngine.AI;

namespace RTS
{
    public class PeasantUnit : UnitBase
    {
        public Action<ResourcesType,int> OnUpdateResource { get; set; }
        [Header("PeasantUnit")]
        public float timeWorkingResource;
        public ResourceNode nodeResources;
        public BuildingBase nodeStorage;
        public int capacityResources = 0;
        public int capacityResourcesMax = 10;

        public List<Transform> utensilResources;//dụng cụ của nông dân
        public float rangeDistance;
        public bool canWorking = true;
    

        // Start is called before the first frame update
        // void Start()
        // {
        //     Initialize();
        // }
        public override void Initialized()
        {
            base.Initialized();
            nodeResources = ResourceNodeManager.Instance.GetTreeNearest(transform);

            nodeStorage = BuildingManager.Instance.listHoused[0];
            capacityResources = 0;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = StateUnit.MovingToResour;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                state = StateUnit.GatheringResources;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                state = StateUnit.MovingToStorage;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                state = StateUnit.FindNodeResources;
            }
            StateHandle();
        }
        void StateHandle()
        {
            //state = stateUnit;
            switch (state)
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
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 1);
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Run_State, 0);

            navAgent.SetDestination(nodeResources.transform.position);
            rangeDistance = nodeResources.distanceTargetStop;
            navAgent.stoppingDistance = rangeDistance;
            if (CanReachPosition(nodeResources.transform.position))
            {

                Debug.Log("completed StateUnit.MovingToResour");
                state = StateUnit.GatheringResources;
            }

        }
        void MovingToStorage()
        {
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 1);
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Run_State, 1);

            navAgent.SetDestination(nodeStorage.transform.position);
            rangeDistance = nodeStorage.distanceTargetStop;
            navAgent.stoppingDistance = rangeDistance;
            if (CanReachPosition(nodeStorage.transform.position))
            {
                Debug.Log("completed StateUnit.MovingToStorage");
                OnUpdateResource?.Invoke(nodeResources.resourcesType,capacityResources);
                capacityResources = 0;
                state = StateUnit.MovingToResour;

            }
        }
        void FindNodeResources()
        {

        }
        void GatheringResources()
        {
            if (nodeResources.resourcesType == ResourcesType.Tree)
            {
                animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 0);
                animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);
                if (canWorking)
                {

                    if (capacityResources >= capacityResourcesMax)
                    {
                        state = StateUnit.MovingToStorage;

                    }
                    else
                    {

                        Debug.Log("working");
                        canWorking = false;
                        this.Wait(timeWorkingResource, () => canWorking = true);
                        animatorHandle.PlayAnimation(StateUnitAnimation.Work1);
                        capacityResources++;
                    }


                }
            }
        }
        public bool CanReachPosition(Vector3 positionTarget)
        {
            return Vector3.Distance(positionTarget, transform.position) <= rangeDistance;
        }
    }
}