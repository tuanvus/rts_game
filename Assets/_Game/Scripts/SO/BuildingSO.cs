using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO")]

public class BuildingSO :  ScriptableObject
{
    [Header("Building Setting")]
    public BuildingType type;
    public new string name;
    public GameObject buildingPrefab;
    public float timeBuildingHouse;
    public float timeSpawnUnit;
    public List<Sprite> iconUnits;
}
public enum BuildingType
{
    BARRACKS
}