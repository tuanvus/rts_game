using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace RTS
{
    public class UI_Gameplay : MonoBehaviour
    {

        [Header("Text UI")]
        public float animationDuration = 1.0f;
        [SerializeField] TextMeshProUGUI textTrees;
        [SerializeField] TextMeshProUGUI textGolds;
        [SerializeField] TextMeshProUGUI textStones;


        void Start()
        {

        }
        public void SetTextTree(int addTree)
        {
            int startValue = int.Parse(textTrees.text);
            DOVirtual.Int(startValue,addTree,animationDuration,(txt) => textTrees.text = txt.ToString());
            //textTrees.Do
        }
        public void SetTextGold(int addGold)
        {
            textGolds.text = addGold.ToString();
        }
        public void SetTextStone(int addStone)
        {
            textStones.text = addStone.ToString();
        }
    }
}