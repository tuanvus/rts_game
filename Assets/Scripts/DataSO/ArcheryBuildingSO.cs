using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS
{
    public class ArcheryBuildingSO : ScriptableObject
    {
        public BuildingCost buildingCost;
        public float timeBuildingHouse;
        public float timeSpawnUnit;
        public List<UnitBase> units;
        public List<Sprite> iconUnits;

        
    }
}