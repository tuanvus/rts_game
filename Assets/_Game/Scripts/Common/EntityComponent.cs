using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
[RequireComponent(typeof(MiniMapDisplay))]
[RequireComponent(typeof(InteractionObject))]
[RequireComponent(typeof(GameEntityInfo))]
[DisallowMultipleComponent]
public class EntityComponent : MonoBehaviour
{

    [TitleHeader("Components")] [SerializeField] protected GameEntityInfo entityInfo;
    [SerializeField] protected HealManager healManager;
    [SerializeField] protected InteractionObject interactionObject;
    [SerializeField] protected UnitInfo unitInfo;

}
