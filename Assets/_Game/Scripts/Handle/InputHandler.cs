using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InputHandler : Singleton<InputHandler>
{
   
    [TitleHeader("Component")]
    [SerializeField] SelectObjectHandle selectObjectHandle;
    [SerializeField] SelectUnitsHandle selectUnitHandle;
 


    void Start()
    {
    }



    private void Update()
    {
        //SelectBuilding();
        selectUnitHandle.SelectUnit();
    }

  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(prefabPosition, prefabToPlace.GetComponent<Collider>().bounds.size);
    }
}