using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public Transform highlightRing;
    void Start()
    {
        
    }

    private void OnValidate()
    {
        highlightRing = transform.Find("Point/Highlight Ring");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
