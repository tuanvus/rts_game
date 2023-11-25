using System;
using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField,ReadOnly] protected Transform target;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Transform tf)
    {
        target = tf;
    }


 
}
