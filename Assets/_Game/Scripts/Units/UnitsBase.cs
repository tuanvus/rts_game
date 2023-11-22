using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;


public class UnitsBase : EntityComponent, IHit
{
    [SerializeField] protected AnimatorHandle animatorHandle;
    [TitleHeader("Config")] [SerializeField] protected UnitSO unitSO;

   // [Button]
    public void SetupField()
    {
        entityInfo = GetComponent<GameEntityInfo>();
        this.TryGetComponentInChildren(out healManager);
        this.TryGetComponentInChildren(out animatorHandle);
        this.TryGetComponentInChildren(out unitInfo);



        string folderPath = "Assets/_Game/SO_Data/Units/";

        string path = folderPath +"/" + transform.name + ".asset";
        if (File.Exists(path))
        {
            unitSO = UnityEditor.AssetDatabase.LoadAssetAtPath<UnitSO>(path);
        }
  
        
        TryGetComponent(out interactionObject);
        this.CopyAttributes(entityInfo, unitSO.entitySerializable);
        this.CopyAttributes(unitInfo, unitSO.entityStatSerializable);
    }

    private void OnValidate()
    {
        SetupField();
    }

    public void TakeDamage(int damage)
    {
    }
}