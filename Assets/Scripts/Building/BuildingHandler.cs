using UnityEngine;

namespace RTS.Buildings
{
    public class BuildingHandler : Singleton<BuildingHandler>
    {




        [SerializeField]
        BuildingBasic barraks;


  

        public BuildingStatType.Base GetBasicBuildingStats(string type)
        {
            BuildingBasic building;
            switch (type)
            {
                case "barrak":
                    building = barraks;
                    break;
               
                default:
                    Debug.Log($"Unity :{type} could not be found ");
                    return null;
            }
            return building.baseStats;
        }

    }


}
}