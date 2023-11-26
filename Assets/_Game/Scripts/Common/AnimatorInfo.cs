using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatorInfo : MonoBehaviour
{
    [FormerlySerializedAs("speed")]
    [TitleHeader("Param")] //------------
    [AnimatorParam]
    public string p_Speed;

    [AnimatorParam] public string p_Wood;
    [AnimatorParam] public string p_Food;
    [AnimatorParam] public string p_Gold;
    [AnimatorParam] public string p_Idle_State;
    [AnimatorParam] public string p_Run_State;


    [TitleHeader("AnimationName")] //-------
    [AnimatorAnim]
    public string ani_Food_Harvest;

    [AnimatorAnim] public string ani_Gold_Mining;
  [AnimatorAnim] public string ani_TreeChopping;
}