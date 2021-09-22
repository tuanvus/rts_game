using UnityEngine;
namespace RTS.Buildings
{
    public class BuildingBasic : ScriptableObject
    {
        public enum buildingType
        {
            Barracks
        }
        [Space(15)]
        [Header("Building Setting")]
        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public BuildingActions.BuildingUnits Units; 

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]
        public BuildingStatType.Base baseStats;

    }
}