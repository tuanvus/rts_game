using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;

[CreateAssetMenu(menuName = "SO/UnitSO")]
public class UnitSO : ScriptableObject
{
    public EntitySerializable entitySerializable;
    public EntityStatSerializable entityStatSerializable;

    [TitleHeader("Prefab")] public GameObject humanPrefab;
    public GameObject infectedPrefab;


    public void OnValidate()
    {
       string nameScript = this.name.ToUpper();
       
    }
}

[System.Serializable]
public class EntityStatSerializable
{
    public float cost;
    public float atkRange;
    public float atkSpeed;
    public int armor;
    public int damage;
    public int health;
    public int healthMax;
    public int critRatio;
    public UnitType type;
    public string unitName;
}

public enum UnitType
{
    ARCHER,
    COMMANDER,
    CROSSBOWMAN,
    HALBERDIER,
    HEAVY_CAVALRY,
    HEAVY_INFANTRY,
    HEAVYSWORDMAN,
    HIGHPRIEST,
    KING,
    LIGHT_CAVALRY,
    LIGHT_INFANTRY,
    MAGE,
    MOUNTED_KING,
    MOUNTED_KNIGHT,
    MOUNTED_MAGE,
    MOUNTED_PALADIN,
    MOUNTED_PRIEST,
    MOUNTED_SCOUT,
    PALADIN,
    PEASANT,
    PRIEST,
    SCOUT,
    SETTLER,
    SPEARMAN,
    SWORDMAN,
}