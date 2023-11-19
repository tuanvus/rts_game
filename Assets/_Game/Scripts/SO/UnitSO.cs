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
    WORKER,
    WARRIOR,
    HEALER
}