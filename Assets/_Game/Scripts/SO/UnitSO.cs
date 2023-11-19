using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO")]
public class UnitSO : ScriptableObject
{
    public float cost, atkRange, atkSpeed;
    public int armor;
    public int damage;
    public int health;
    public int healthMax;
    public int critRatio;
    public UnitType type;
    public string unitName;
    public GameObject humanPrefab;
    public GameObject infectedPrefab;
}

public enum UnitType
{
    WORKER,
    WARRIOR,
    HEALER
}