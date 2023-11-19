using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(MiniMapDisplay))]
[RequireComponent(typeof(InteractionObject))]
public class UnitsBase : MonoBehaviour,IHit
{
    [SerializeField] protected GameEntityInfo entityInfo;
    [SerializeField] protected HealManager healManager;
    [SerializeField] protected UnitSO unitSO;
    [FormerlySerializedAs("animationHandle")] [SerializeField] protected AnimatorHandle animatorHandle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
    }
}
