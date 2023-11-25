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
    [SerializeField,ReadOnly] float rangeDistance;
    [SerializeField,ReadOnly] private AnimatorHandle animatorHandle;
    [SerializeField,ReadOnly] private NavMeshAgent navAgent;
    [SerializeField] bool isGathering;
    [SerializeField] bool isAttack;
    
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

        
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.run_State, 1);

        navAgent.SetDestination(target.transform.position);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        navAgent.autoBraking = false;
        StartCoroutine(WaitForDestinationReached());
    }

   public  void MovingToTarget(Vector3 target)
    {
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 1);
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.run_State, 1);

        navAgent.SetDestination(target);
        rangeDistance = 1;
        navAgent.stoppingDistance = rangeDistance;
        navAgent.autoBraking = false;
        StartCoroutine(WaitForDestinationReached());
    }
    
    IEnumerator WaitForDestinationReached()
    {
        while (true)
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
        // Hàm được gọi khi agent đến đích
        Debug.Log("Agent đã đến đích!");
        animatorHandle.SetFloatAnimation(animatorHandle.GetParam.speed, 0);
        onCompleteMove?.Invoke();
    }
}