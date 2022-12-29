using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS

{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] public int Tree { get; private set; }
        [SerializeField] public int Gold { get; private set; }
        [SerializeField] public int Stone { get; private set; }

        public void Initialize()
        {
            Tree = 100;
            Gold = 100;
            Stone = 100;
            UI_Manager.Instance.ui_Gameplay.SetTextTree(Tree);
            UI_Manager.Instance.ui_Gameplay.SetTextGold(Tree);
            UI_Manager.Instance.ui_Gameplay.SetTextStone(Tree);

        }
        public void OnUpdateResource(ResourcesType resourceType, int value)
        {
            Debug.Log("Resource =" + resourceType);
            switch (resourceType)
            {
                case ResourcesType.Gold:
                    Gold += value;
                    UI_Manager.Instance.ui_Gameplay.SetTextGold(Gold);
                    break;
                case ResourcesType.Stone:
                    Stone += value;
                    UI_Manager.Instance.ui_Gameplay.SetTextStone(Stone);
                    break;
                case ResourcesType.Tree:
                    Tree += value;
                    UI_Manager.Instance.ui_Gameplay.SetTextTree(Tree);
                    break;
                case ResourcesType.Food:
                    break;
                default:
                    break;

            }

        }
        public void AddTree(int tree)
        {
            Tree += tree;
            UI_Manager.Instance.ui_Gameplay.SetTextTree(Tree);
        }
        public void AddGold(int gold)
        {
            Gold += gold;
            UI_Manager.Instance.ui_Gameplay.SetTextGold(Gold);
        }
        public void AddStone(int stone)
        {
            Stone += stone;
            UI_Manager.Instance.ui_Gameplay.SetTextStone(Stone);
        }
    }
}