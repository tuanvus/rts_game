using System.Collections;
using System.Collections.Generic;
using System.IO;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class BuildingBase : EntityComponent, IHit
{
    [TitleHeader("Config")] [SerializeField] protected BuildingSO buildingSO;

    [Button]
    public void SetupField()
    {
        entityInfo = GetComponent<GameEntityInfo>();
        this.TryGetComponentInChildren(out healManager);
        this.TryGetComponentInChildren(out unitInfo);
        string folderPath = "Assets/_Game/SO_Data/Building/";

        string nameFile = transform.name.Replace("Building", "");
        string path = folderPath + "/" + nameFile+ ".asset";
        if (File.Exists(path))
        {
            buildingSO = UnityEditor.AssetDatabase.LoadAssetAtPath<BuildingSO>(path);
        }

        TryGetComponent(out interactionObject);
       // this.CopyAttributes(entityInfo, buildingSO.entitySerializable);
       // this.CopyAttributes(unitInfo, buildingSO.entityStatSerializable);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}

