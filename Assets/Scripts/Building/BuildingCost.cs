using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [System.Serializable]
    public class BuildingCost
    {
        public int woodCost;
        public int foodCost;
        public int goldCost;
        public int stoneCost;
        public BuildingCost()
        {

        }

        public BuildingCost(int woodCost, int foodCost, int goldCost, int stoneCost)
        {
            this.woodCost = woodCost;
            this.foodCost = foodCost;
            this.goldCost = goldCost;
            this.stoneCost = stoneCost;
        }

        public bool CanBuilding(BuildingCost building, int wood, int food, int gold, int stone)
        {
            if (woodCost >= building.woodCost && foodCost >= building.foodCost && goldCost >= building.goldCost && stoneCost >= building.stoneCost)
            {
                return true;
            }

            return false;

        }

    }
}