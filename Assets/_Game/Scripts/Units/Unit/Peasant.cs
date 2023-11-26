using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peasant : UnitsBase
{
    [SerializeField] private ResourceGatheringBehavior gatheringBehavior;

    [SerializeField] private bool isGathering;
    [SerializeField] private bool isBuilding;

    [SerializeField] NodeResource nodeResource;
    [SerializeField] BuildingBase buildingBase;

    protected override void OnValidate()
    {
        base.OnValidate();
        this.TryGetComponentInChildren(out gatheringBehavior);
    }

    protected override void Start()
    {
        base.Start();
        gatheringBehavior.Initialization(animatorHandle);
        gatheringBehavior.OnMove += MoveToBuilding;
        movementBehavior.onCompleteMove += OnCompleteMove;
    }


    private void OnCompleteMove()
    {
        Debug.Log("isGathering  =" + isGathering + " ---- isBuilding = " + isBuilding);
        if (isGathering)
        {
            Debug.Log("dis " + Vector3.Distance(transform.position, nodeResource.transform.position));
            // Debug.Log("v");
            if (nodeResource.Amount > 0)
            {
                gatheringBehavior.GatheringResources(nodeResource);
            }
            else
            {
                animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Gold, false);
                animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Food, false);
                animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Wood, false);
            }
        }
        else if (isBuilding)
        {
            if (nodeResource.Amount > 0)
            {
                MoveToResource(nodeResource.transform);
            }
            else
            {
                if (ResourceManager.Instance.GetNodeResource(nodeResource.transform) != null)
                {
                    Debug.Log("if");

                    nodeResource = ResourceManager.Instance.GetNodeResource(nodeResource.transform);
                    MoveToResource(nodeResource.transform);

                }
                else
                {
                    Debug.Log("else");

                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Gold, false);
                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Food, false);
                    animatorHandle.SetBoolAnimation(animatorHandle.Data.p_Wood, false);  
                }
              
            }
        }
    }

    public override void Movement(Transform target)
    {
        if (target.GetComponent<NodeResource>() != null)
        {
            nodeResource = target.GetComponent<NodeResource>();
            MoveToResource(target);
        }
        else if (target.GetComponent<BuildingBase>() != null)
        {
            MoveToBuilding(target);
        }
    }

    public void MoveToResource(Transform pos)
    {
        isBuilding = false;
        isGathering = true;
        movementBehavior.MovingToTarget(pos);
    }

    private void MoveToBuilding(Transform pos)
    {
        movementBehavior.MovingToTarget(pos);

        isBuilding = true;
        isGathering = false;
    }
}