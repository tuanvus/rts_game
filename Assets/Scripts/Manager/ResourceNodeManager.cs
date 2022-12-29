using System.Collections.Generic;
using RTS.Resource;
using UnityEngine;


/// <summary>
/// 
/// </summary>
//[System.Serializable]
namespace RTS
{

    public class ResourceNodeManager : Singleton<ResourceNodeManager>
    {
        public List<ResourceNode> nodeTree;
        public List<ResourceNode> nodeFood;
        public List<ResourceNode> nodeGold;
        public List<ResourceNode> nodeStone;
        public List<ResourceNode> nodeFraming;

        public ResourceNodeManager()
        {

        }

        public ResourceNode GetTreeNearest(Transform unit)
        {
            for (int i = 0; i < nodeTree.Count; i++)
            {
                if (Vector3.Distance(nodeTree[i].transform.position, unit.position) < 30)
                {
                    return nodeTree[i];
                }
            }
            return null;

        }
        public ResourceNode GetFoodNearest(Transform unit)
        {
            for (int i = 0; i < nodeFood.Count; i++)
            {
                if (Vector3.Distance(nodeFood[i].transform.position, unit.position) < 30)
                {
                    return nodeFood[i];
                }
            }
            return null;

        }
        public ResourceNode GetGoldNearest(Transform unit)
        {
            for (int i = 0; i < nodeGold.Count; i++)
            {
                if (Vector3.Distance(nodeGold[i].transform.position, unit.position) < 30)
                {
                    return nodeGold[i];
                }
            }
            return null;

        }
        public ResourceNode GetStoneNearest(Transform unit)
        {
            for (int i = 0; i < nodeStone.Count; i++)
            {
                if (Vector3.Distance(nodeStone[i].transform.position, unit.position) < 30)
                {
                    return nodeStone[i];
                }
            }
            return null;

        }
        public ResourceNode GetFramingNearest(Transform unit)
        {
            for (int i = 0; i < nodeFraming.Count; i++)
            {
                if (Vector3.Distance(nodeFraming[i].transform.position, unit.position) < 30)
                {
                    return nodeFraming[i];
                }
            }
            return null;

        }

    }
}
