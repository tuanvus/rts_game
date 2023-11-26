using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehavior : StateHandler
{
    public Action onCompleteMove;

    // Start is called before the first frame update
    [SerializeField, ReadOnly] float rangeDistance;
    [SerializeField, ReadOnly] private AnimatorHandle animatorHandle;
    [SerializeField, ReadOnly] private NavMeshAgent navAgent;
    bool isMove = false;

    void Start()
    {
    }


    public void Initialization(AnimatorHandle animatorHandle, NavMeshAgent navAgent)
    {
        this.animatorHandle = animatorHandle;
        this.navAgent = navAgent;
    }


    public void MovingToTarget(Transform target)
    {
        isMove = true;
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Run_State, 1);

        navAgent.SetDestination(target.transform.position);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        navAgent.autoBraking = false;
        StartCoroutine(WaitForDestinationReached());
    }

    public void MovingToTarget(Vector3 target)
    {
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Run_State, 1);

        navAgent.SetDestination(target);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        navAgent.autoBraking = false;
        StartCoroutine(WaitForDestinationReached());
    }

    IEnumerator WaitForDestinationReached()
    {
        while (isMove)
        {
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                // Agent đã đến đích
                OnDestinationReached();
                yield break;
            }

            yield return null;
        }
    }

    private void OnDestinationReached()
    {
        isMove = false;
        // Hàm được gọi khi agent đến đích
        Debug.Log("Agent Complete Move");
        animatorHandle.SetFloatAnimation(animatorHandle.Data.p_Speed, 0);
        onCompleteMove?.Invoke();
    }
}