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
        public StateBuiding stateBuiding;
       [FoldoutGroup("BuildingBase")] public Transform root;
        [FoldoutGroup("BuildingBase")] public Transform flag;

        [FoldoutGroup("BuildingBase")] public int level;
        [FoldoutGroup("BuildingBase")] public GameObject unitsStatsDisplay;
        [FoldoutGroup("BuildingBase")] public Image healthBarAmount;
        [FoldoutGroup("BuildingBase")] public float distanceTargetStop;
        [FoldoutGroup("BuildingBase")] public List<GameObject> houseBuildingAction;
        [FoldoutGroup("BuildingBase")] public int HPMax;
        [FoldoutGroup("BuildingBase")] public int HP;
        [FoldoutGroup("BuildingBase")] public float timeSpawnUnit;
        [FoldoutGroup("BuildingBase")] public float timeConstructionMax;
        [FoldoutGroup("BuildingBase")] public float preTimeConstruction;
        [FoldoutGroup("BuildingBase")] public BuildingCost buildingCost;






        protected void Reset()
        {
            Debug.Log("Reset Data");
            LoadComponents();
            ResetValue();
        }

        protected virtual void ResetValue()
        {
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



        void Start()
        {

        }

        void Update()
        {

        }

        public void SpawnUnits()
        {

        }    



    }
}