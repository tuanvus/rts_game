using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace RTS
{



    public enum StateBuiding
    {
        Construction,
        Active,
        Be_Attack,
        Broken,
        Destroy

    }
    public class BuildingBase : MonoBehaviour
    {
        protected string path = "DataSO/Building/";
        public StateBuiding stateBuiding;
        [FoldoutGroup("BuildingBase")] public int level;
        [FoldoutGroup("BuildingBase")] public GameObject unitsStatsDisplay;
        [FoldoutGroup("BuildingBase")] public Image healthBarAmount;
        [FoldoutGroup("BuildingBase")] public BuildingCost buildingCost;

        [ShowIf("stateBuiding", StateBuiding.Construction)][FoldoutGroup("BuildingBase")] public List<GameObject> houseBuildingAction;
        [ShowIf("stateBuiding", StateBuiding.Construction)][FoldoutGroup("BuildingBase")] public float timeConstructionMax;
        [ShowIf("stateBuiding", StateBuiding.Construction)][FoldoutGroup("BuildingBase")] public float preTimeConstruction;

        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public Transform root;
        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public Transform flag;
        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public float distanceTargetStop;
        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public int HPMax;
        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public int HP;
        [ShowIf("stateBuiding", StateBuiding.Active)][FoldoutGroup("BuildingBase")] public float timeSpawnUnit;

        protected void Reset()
        {
            Debug.Log("Reset Data");
            ResetValue();

            LoadComponents();
            SetDataSO();
        }

        protected virtual void ResetValue()
        {
            path = "DataSO/Building/" + this.name + "SO";
            Debug.Log(path);
        }
        void SetDataSO()
        {
            Debug.Log("LoadComponents = " + path);
            Debug.Log("L =" + Resources.Load<BuildingSO>(path).name);
           var buildingSO = LoadDataSO() as BuildingSO;
            buildingCost = new BuildingCost(buildingSO.buildingCost.woodCost, buildingSO.buildingCost.foodCost,
                buildingSO.buildingCost.goldCost, buildingSO.buildingCost.stoneCost);
        }
        protected virtual ScriptableObject LoadDataSO()
        {
            return Resources.Load<ScriptableObject>(path) as ScriptableObject;
        }
        protected virtual void LoadComponents()
        {
            houseBuildingAction = new List<GameObject>();
            root = transform.Find("Root");
            flag = transform.Find("flag");
            unitsStatsDisplay = transform.Find("UnitsStatsDisplay").gameObject;
            healthBarAmount = unitsStatsDisplay.transform.Find("HealthBar").GetComponent<Image>();
            foreach (Transform item in root)
            {
                houseBuildingAction.Add(item.gameObject);
            }



        }
        protected virtual void LoadResourcesSO()
        {

            // archeryBuildingSO = Resources.Load<ArcheryBuildingSO>(path);
            // buildingCost = new BuildingCost(archeryBuildingSO.buildingCost.woodCost, archeryBuildingSO.buildingCost.foodCost,
            //     archeryBuildingSO.buildingCost.goldCost, archeryBuildingSO.buildingCost.stoneCost);
        }


        protected virtual void Start()
        {
            Initialized();
        }

        void Update()
        {

        }
        protected virtual void Initialized()
        {
            stateBuiding = StateBuiding.Construction;
            HP = HPMax;
            preTimeConstruction = 0;
            timeSpawnUnit = 0;
            if (unitsStatsDisplay == null)
            {
            }
            else
            {
                unitsStatsDisplay.SetActive(true);
            }
            StartCoroutine(ConstructionHouse());
        }

        IEnumerator ConstructionHouse()
        {
            while (preTimeConstruction < timeConstructionMax)
            {
                preTimeConstruction += Time.deltaTime;
                Debug.Log("ConstructionHouse === " + GetPercentConstruction());
                if (GetPercentConstruction() >= 1 && GetPercentConstruction() < 60)
                {
                    houseBuildingAction[0].SetActive(true);
                }
                else if (GetPercentConstruction() >= 60 && GetPercentConstruction() < 90)
                {
                    houseBuildingAction[0].SetActive(false);
                    houseBuildingAction[1].SetActive(true);
                }
                else if (GetPercentConstruction() >= 90 && GetPercentConstruction() < 100)
                {
                    houseBuildingAction[0].SetActive(false);
                    houseBuildingAction[1].SetActive(false);
                    houseBuildingAction[2].SetActive(true);
                }


                yield return null;
            }
            stateBuiding = StateBuiding.Active;
            unitsStatsDisplay.SetActive(false);
        }

        int GetPercentConstruction()
        {
            return (int)(preTimeConstruction / timeConstructionMax * 100);
        }

    }
}