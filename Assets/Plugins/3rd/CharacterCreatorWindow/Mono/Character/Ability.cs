using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public AbilityEnum abilityType = AbilityEnum.mage;
    
    public Animator animator;

    public int damage;

    public void SetDamage(int val)
    {
        damage = val;
    }
}

public enum AbilityEnum
{
    mage, warrior, scout
}
