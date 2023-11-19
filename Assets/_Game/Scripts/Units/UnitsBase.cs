using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(MiniMapDisplay))]
[RequireComponent(typeof(InteractionObject))]
[RequireComponent(typeof(GameEntityInfo))]
public class UnitsBase : MonoBehaviour, IHit
{
    [TitleHeader("Components")] [SerializeField] protected GameEntityInfo entityInfo;
    [SerializeField] protected HealManager healManager;
    [SerializeField] protected AnimatorHandle animatorHandle;
    [SerializeField] protected InteractionObject interactionObject;
    [SerializeField] protected UnitInfo unitInfo;
    [TitleHeader("Config")] [SerializeField] protected UnitSO unitSO;

    void Start()
    {
    }


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