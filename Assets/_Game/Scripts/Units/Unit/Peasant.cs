using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peasant : UnitsBase
{
    [SerializeField] private ResourceGatheringBehavior gatheringBehavior;
    [SerializeField] private bool isGathering;
    protected override void OnValidate()
    {
        base.OnValidate();
        this.TryGetComponentInChildren(out gatheringBehavior);
    }

    private void Start()
    {
        gatheringBehavior.OnMove += MoveToResource;
        movementBehavior.onCompleteMove += OnCompleteMove;
    }

    private void OnCompleteMove()
    {
        if (isGathering)
        {
            gatheringBehavior.GatheringResources();
        }
        else
        {
            //attackBehavior
        }
    }

    private void MoveToResource(Vector3 pos)
    {
        Movement(pos);
    }
}
