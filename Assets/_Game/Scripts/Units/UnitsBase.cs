using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.Serialization;


public class UnitsBase : EntityComponent, IHit
{
    [SerializeField] protected AnimatorHandle animatorHandle;
    [TitleHeader("Config")] [SerializeField] protected UnitSO unitSO;

    [Button]
    public void SetupField()
    {
        entityInfo = GetComponent<GameEntityInfo>();
        this.TryGetComponentInChildren(out healManager);
        this.TryGetComponentInChildren(out animatorHandle);
        this.TryGetComponentInChildren(out unitInfo);

        TryGetComponent(out interactionObject);
        this.CopyAttributes(entityInfo, unitSO.entitySerializable);
        this.CopyAttributes(unitInfo, unitSO.entityStatSerializable);
    }

    public void TakeDamage(int damage)
    {
    }
}