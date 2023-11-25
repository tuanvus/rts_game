using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class AnimatorInfo : MonoBehaviour
{
    [TitleHeader("Param")] [AnimatorParam] public string speed;
    [AnimatorParam] public string wood;
    [AnimatorParam] public string food;
    [AnimatorParam] public string gold;
    [AnimatorParam] public string idle_State;
    [AnimatorParam] public string run_State;

    [TitleHeader("AnimationName")] //-------
    [AnimatorAnim]
    public string food_Harvest;

    [AnimatorAnim] public string gold_Mining;
    [AnimatorAnim] public string treeChopping;
}