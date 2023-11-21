using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BuildingSO")]

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
    ARCHERY,BARRACKS,BLACKSMITH,CASTLE,FARM,GRANARY,HOUSE,KEEP,LIBRARY,LUMBERMILL,MAGETOWER,MARKET,STABLES,TEMPLE,TOWER_A,TOWNHALL,WALL_A_1X1,WORKSHOP,
}