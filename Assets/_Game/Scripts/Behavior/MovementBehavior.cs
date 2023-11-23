using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : StateHandler
{
    // Start is called before the first frame update
    [SerializeField] float rangeDistance;
    [SerializeField] private AnimatorHandle animatorHandle;
    [SerializeField] private NavMeshAgent navAgent;

    void Start()
    {
    }

    public bool CanReachPosition(Vector3 positionTarget)
    {
        return Vector3.Distance(positionTarget, transform.position) <= rangeDistance;
    }

    void MovingToTarget(Transform target)
    {
        animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 1);
        animatorHandle.SetFloatAnimation(StateUnitAnimation.Run_State, 1);

        navAgent.SetDestination(target.transform.position);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        if (CanReachPosition(target.transform.position))
        {
            // OnUpdateResource?.Invoke(nodeResources.resourcesType, capacityResources);
            //capacityResources = 0;
            // state = StateUnit.MovingToResour;
            //GetUtensilResources();
        }
    }

    void MovingToTarget(Vector3 target)
    {
        animatorHandle.SetFloatAnimation(StateUnitAnimation.Speed, 1);
        animatorHandle.SetFloatAnimation(StateUnitAnimation.Run_State, 1);

        navAgent.SetDestination(target);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        if (CanReachPosition(target))
        {
            // OnUpdateResource?.Invoke(nodeResources.resourcesType, capacityResources);
            //capacityResources = 0;
            // state = StateUnit.MovingToResour;
            //GetUtensilResources();
        }
    }
}