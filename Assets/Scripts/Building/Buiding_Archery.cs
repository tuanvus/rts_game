using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS.Buildings
{
    public class Buiding_Archery : BuildingBase
    {

        [FoldoutGroup("Buiding_Archery")]  public ArcheryBuildingSO archeryBuildingSO;


        protected override void LoadComponents()
        {
            base.LoadComponents();
            string path = "DataSO/ArcheryBuildingSO";
            archeryBuildingSO = Resources.Load<ArcheryBuildingSO>(path);
            buildingCost = new BuildingCost(archeryBuildingSO.buildingCost.woodCost,archeryBuildingSO.buildingCost.foodCost,
                archeryBuildingSO.buildingCost.goldCost,archeryBuildingSO.buildingCost.stoneCost);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}