using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class ResourceGatheringBehavior : StateHandler
{
    public Action<Vector3> OnMove;
    
    [SerializeField] AnimatorHandle animatorHandle;
    [SerializeField] NodeResource nodeResources;
    [SerializeField] NodeResource nodeStorage;


    [SerializeField] public Transform targetPosition;


    [SerializeField] List<Transform> utensilResources; //dụng cụ của nông dân

    [SerializeField] int capacityResources = 0;
    [SerializeField] int capacityResourcesMax = 10;

    [SerializeField] float timeWorkingResource;
    [SerializeField] float rangeDistance;

    [SerializeField] bool canWorking = true;

    void Start()
    {
    }

    void Update()
    {
    }

    void MovingToResources()
    {
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.run_State, 0);
        //  navAgent.SetDestination(nodeResources.transform.position);
        //rangeDistance = nodeResources.distanceTargetStop;
        //  navAgent.stoppingDistance = rangeDistance;
        if (CanReachPosition(nodeResources.transform.position))
        {
            //   state = StateUnit.GatheringResources;
        }
    }

    void MovingToStorage()
    {
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.run_State, 1);

        //  navAgent.SetDestination(nodeStorage.transform.position);
        //  rangeDistance = nodeStorage.distanceTargetStop;
        //  navAgent.stoppingDistance = rangeDistance;
        if (CanReachPosition(nodeStorage.transform.position))
        {
            // OnUpdateResource?.Invoke(nodeResources.resourcesType, capacityResources);
            capacityResources = 0;
            //   state = StateUnit.MovingToResour;
            GetUtensilResources();
        }
    }

    void FindNodeResources()
    {
    }

   public void GatheringResources()
    {
        // Debug.Log("GatheringResources =" + nodeResources.resourcesType);
        // switch (nodeResources.resourcesType)
        // {
        //     case ResourcesType.Wood:
        //         GatheringResourcesWood();
        //         break;
        //     case ResourcesType.Food:
        //         GatheringResourcesFood();
        //         break;
        //     case ResourcesType.Gold:
        //         GatheringResourcesGold();
        //         break;
        //     case ResourcesType.Stone:
        //         break;
        //     default:
        //         Debug.Log("GatheringResources nullll=" + nodeResources.resourcesType);
        //
        //         break;
        // }
    }

    void GatheringResourcesWood()
    {
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 0);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.idle_State, 1);
        if (canWorking)
        {
            if (capacityResources >= capacityResourcesMax)
            {
              //  animatorHandle.SetBoolAnimation(animatorHandle.GetParam.Work_Wood, false);
                //  state = StateUnit.MovingToStorage;
                //GetUtensilResources(nodeResources.resourcesType, true);
            }
            else
            {
                Debug.Log("working");
                canWorking = false;
                this.Wait(timeWorkingResource, () => canWorking = true);
               // animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Wood, true);
                capacityResources++;
                //GetUtensilResources(nodeResources.resourcesType);
            }
        }
    }

    void GatheringResourcesFood()
    {
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 0);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.idle_State, 1);
        if (canWorking)
        {
            canWorking = false;
            this.Wait(timeWorkingResource, () => canWorking = true);
            if (capacityResources >= capacityResourcesMax)
            {
                //animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Food, false);
                //  state = StateUnit.MovingToStorage;
                //  GetUtensilResources(nodeResources.resourcesType, true);
            }
            else
            {
                // GetUtensilResources(nodeResources.resourcesType);

                Debug.Log("working");
                canWorking = false;
                this.Wait(timeWorkingResource, () => canWorking = true);
               // animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Food, true);
                capacityResources++;
            }
        }
    }

    void GatheringResourcesGold()
    {
      //  animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 0);
      //  animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);
        if (canWorking)
        {
            if (capacityResources >= capacityResourcesMax)
            {
          //      animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Gold, false);
                // state = StateUnit.MovingToStorage;
                GetUtensilResources(nodeResources.gatherType, true);
            }
            else
            {
                //  GetUtensilResources(nodeResources.resourcesType);
                canWorking = false;
                this.Wait(timeWorkingResource, () => canWorking = true);
              //  animatorHandle.SetBoolAnimation(StateUnitAnimation.Work_Gold, true);
                capacityResources++;
            }
        }
    }

    void GetUtensilResources(GatherType resourcesType = GatherType.NONE, bool isStoraged = false)
    {
        foreach (var item in utensilResources)
        {
            item.gameObject.SetActive(false);
        }

        switch (resourcesType)
        {
            case GatherType.NONE:
                foreach (var item in utensilResources)
                {
                    item.gameObject.SetActive(false);
                }

                break;
            case GatherType.WOOD:
                utensilResources[isStoraged != true ? 0 : 1].gameObject.SetActive(false);
                utensilResources[isStoraged == true ? 1 : 0].gameObject.SetActive(true);
                break;
            case GatherType.FOOD:
                utensilResources[isStoraged != true ? 3 : 2].gameObject.SetActive(false);
                utensilResources[isStoraged == true ? 2 : 3].gameObject.SetActive(true);
                break;
            case GatherType.GOLD:
                utensilResources[isStoraged != true ? 4 : 5].gameObject.SetActive(false);
                utensilResources[isStoraged == true ? 5 : 4].gameObject.SetActive(true);
                break;
            case GatherType.STONE:
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

    protected void GetTypeTarget<T>(T target)
    {
       // animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 1);
      //  animatorHandle.SetFloatAnimation(StateUnitAnimation.Idle_State, 1);

        if (target is NodeResource)
        {
            nodeResources = target as NodeResource;
            targetPosition = null;
            // MoveUnit(nodeResources.transform, nodeResources.distanceTargetStop);
        }

        if (target is Transform)
        {
            targetPosition = target as Transform;
            nodeResources = null;
            // MoveUnit(targetPosition);
        }
    }
}