using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityInfo : MonoBehaviour
{
    public string unitName;

    public string description;

    public int foodCost;

    public int woodCost;

    public int goldCost;

    public float buildTime;

    public int minTier;
}

[System.Serializable]
public class EntitySerializable
{
    public string unitName;

    public string description;

    public int foodCost;

    public int woodCost;

    public int goldCost;

    public float buildTime;

    public int minTier;
}