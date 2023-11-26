using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;


public class UnitsBase : EntityComponent, IHit
{
    [SerializeField] protected AnimatorHandle animatorHandle;
    [SerializeField] protected NavMeshAgent agent;
    [TitleHeader("Config")] [SerializeField] protected UnitSO unitSO;
    [SerializeField] protected bool isAttack;
    [TitleHeader("Behavior")] //------------------
    [SerializeField]
    protected MovementBehavior movementBehavior;

    [SerializeField] protected AttackBehavior attackBehavior;

    // [Button]
     void SetupField()
    {
        entityInfo = GetComponent<GameEntityInfo>();
        agent  = GetComponent<NavMeshAgent>();
        this.TryGetComponentInChildren(out healManager);
        this.TryGetComponentInChildren(out animatorHandle);
        this.TryGetComponentInChildren(out unitInfo);
        this.TryGetComponentInChildren(out movementBehavior);
        this.TryGetComponentInChildren(out attackBehavior);


        string folderPath = "Assets/_Game/SO_Data/Units/";

        string path = folderPath + "/" + transform.name + ".asset";
        if (File.Exists(path))
        {
            unitSO = UnityEditor.AssetDatabase.LoadAssetAtPath<UnitSO>(path);
        }


        TryGetComponent(out interactionObject);
        this.CopyAttributes(entityInfo, unitSO.entitySerializable);
        this.CopyAttributes(unitInfo, unitSO.entityStatSerializable);
    }


    protected virtual void OnValidate()
    {
        SetupField();
    }

    protected virtual void Start()
    {
        movementBehavior.Initialization(animatorHandle, agent);
    }

    public virtual void Movement(Transform target)
    {
    }
    public void Attack(Transform target)
    {
        //attackBehavior.AttackTarget(target);
    }

    public void TakeDamage(int damage)
    {
    }
}