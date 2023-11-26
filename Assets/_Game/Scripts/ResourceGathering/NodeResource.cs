using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeResource : MonoBehaviour, IResource
{
    public GatherType gatherType;
    public Animator animator;

    public int Amount
    {
        get => amount;
    }

    [SerializeField] private int amount;

    void Start()
    {
    }

    public void DecreaseAmount()
    {
        amount--;
        animator.DoIfNotNull(() => { animator.CrossFadeInFixedTime("TreeHit", 0.1f); });

        if (amount == 0)
        {
            if (gatherType != GatherType.WOOD)
            {
                gameObject.SetActive(false);
            }
            else
            {
                animator.DoIfNotNull(() => { animator.CrossFadeInFixedTime("TreeFalling", 0.1f); });
                this.Wait(3.5f, () => { gameObject.SetActive(false); });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}