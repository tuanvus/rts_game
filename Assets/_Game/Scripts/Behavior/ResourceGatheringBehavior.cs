using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class ResourceGatheringBehavior : StateHandler
{
    public Action<Transform> OnMove;

    [SerializeField] AnimatorHandle animatorHandle;
    [SerializeField] NodeResource nodeResources;
    [SerializeField] BuildingBase buildingBase;

    [SerializeField] List<Transform> utensilResources; //dụng cụ của nông dân

    [SerializeField] int capacityResources = 0;
    [SerializeField] int capacityResourcesMax = 10;

    [SerializeField] bool canWorking = true;
    [SerializeField] float timeWorking = 0;
    void Start()
    {
    }

    public void Initialization(AnimatorHandle animatorHandle)
    {
        this.animatorHandle = animatorHandle;
    }

    void Update()
    {
        if (canWorking)
        {
            timeWorking += Time.deltaTime;
            if (timeWorking >= 1)
            {
                timeWorking = 0;
                capacityResources++;
                nodeResources.DecreaseAmount();
                if (capacityResources >= capacityResourcesMax)
                {
                    capacityResources = capacityResourcesMax;
                    canWorking = false;
                    //GetUtensilResources(nodeResources.gatherType, true);
                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Gold, false);
                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Food, false);
                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Wood, false);
                    OnMove?.Invoke(buildingBase.transform);


                }
            }
        }

    }


    public void GatheringResources(NodeResource nodeResource)
    {
        this.nodeResources = nodeResource;
        Debug.Log("GatheringResources =" + nodeResources.gatherType);
        switch (nodeResources.gatherType)
        {
            case GatherType.WOOD:
                Excute(animatorHandle.Data.p_Wood);
                break;
            case GatherType.FOOD:
                Excute(animatorHandle.Data.p_Food);
                break;
            case GatherType.GOLD:
                Excute(animatorHandle.Data.p_Gold);
                break;
            case GatherType.STONE:
                break;
            default:
                Debug.Log("GatheringResources nullll=" + nodeResources.gatherType);
                break;
        }
    }

    void Excute(string nameAni)
    {
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Speed, 0);
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Idle_State, 1);
        if (!canWorking)
        {
            canWorking = true;
            animatorHandle.SetBoolAnimation(nameAni, true);
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
}