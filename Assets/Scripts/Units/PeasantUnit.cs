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
        public Action<ResourcesType, int> OnUpdateResource { get; set; }
        [Header("PeasantUnit")]
        public ResourcesType testRs;
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
            switch (testRs)
            {
                case ResourcesType.Wood:
                    nodeResources = ResourceNodeManager.Instance.GetWoodNearest(transform);
                    break;
                case ResourcesType.Food:
                    nodeResources = ResourceNodeManager.Instance.GetFoodNearest(transform);
                    break;
                case ResourcesType.Gold:
                    nodeResources = ResourceNodeManager.Instance.GetGoldNearest(transform);
                    Debug.Log("vao ="+ nodeResources.name);
                    break;
                case ResourcesType.Stone:
                    nodeResources = ResourceNodeManager.Instance.GetStoneNearest(transform);
                    break;
            }

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
                OnUpdateResource?.Invoke(nodeResources.resourcesType, capacityResources);
                capacityResources = 0;
                state = StateUnit.MovingToResour;

            }
        }
        void FindNodeResources()
        {

        }
        void GatheringResources()
        {
            switch (nodeResources.resourcesType)
            {
                case ResourcesType.Wood:
                    GatheringResourcesWood();
                    break;
                case ResourcesType.Food:
                    GatheringResourcesFood();
                    break;
                case ResourcesType.Gold:
                    GatheringResourcesGold();
                    break;
                case ResourcesType.Stone:
                    break;


            }

        }


        void GatheringResourcesWood()
        {
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 0);
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);
            if (canWorking)
            {

                if (capacityResources >= capacityResourcesMax)
                {
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Wood, false);
                    state = StateUnit.MovingToStorage;
                    GetUtensilResources(nodeResources.resourcesType,true);

                }
                else
                {

                    Debug.Log("working");
                    canWorking = false;
                    this.Wait(timeWorkingResource, () => canWorking = true);
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Wood, true);
                    capacityResources++;
                    GetUtensilResources(nodeResources.resourcesType);

                }


            }
        }
        void GatheringResourcesFood()
        {
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 0);
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);
            if (canWorking)
            {

                if (capacityResources >= capacityResourcesMax)
                {
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Food, false);
                    state = StateUnit.MovingToStorage;
                    GetUtensilResources(nodeResources.resourcesType,true);

                }
                else
                {
                    GetUtensilResources(nodeResources.resourcesType);

                    Debug.Log("working");
                    canWorking = false;
                    this.Wait(timeWorkingResource, () => canWorking = true);
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Food, true);
                    capacityResources++;
                }

            }
        }
        void GatheringResourcesGold()
        {
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 0);
            animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);
            if (canWorking)
            {

                if (capacityResources >= capacityResourcesMax)
                {
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Gold, false);
                    state = StateUnit.MovingToStorage;
                    GetUtensilResources(nodeResources.resourcesType, true);
                }
                else
                {
                    GetUtensilResources(nodeResources.resourcesType);
                    Debug.Log("working");
                    canWorking = false;
                    this.Wait(timeWorkingResource, () => canWorking = true);
                    animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Gold, true);
                    capacityResources++;
                }

            }
        }
        void GetUtensilResources(ResourcesType resourcesType, bool isStoraged = false)
        {
            switch (resourcesType)
            {
                case ResourcesType.Wood:
                    utensilResources[isStoraged != true ? 0 : 1].gameObject.SetActive(false);
                     utensilResources[isStoraged == true ? 1 : 0].gameObject.SetActive(true);
                     break;
                case ResourcesType.Food:
                    utensilResources[isStoraged != true ? 3 : 2].gameObject.SetActive(false);
                     utensilResources[isStoraged == true ? 2 : 3].gameObject.SetActive(true);
                     break;
                case ResourcesType.Gold:
                    utensilResources[isStoraged != true ? 5 : 4].gameObject.SetActive(false);
                     utensilResources[isStoraged == true ? 4 : 5].gameObject.SetActive(true);
                        break;
                case ResourcesType.Stone:
                    utensilResources[isStoraged != true ? 6 : 4].gameObject.SetActive(false);
                     utensilResources[isStoraged == true ? 4 : 6].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public bool CanReachPosition(Vector3 positionTarget)
        {
            return Vector3.Distance(positionTarget, transform.position) <= rangeDistance;
        }
    }
}