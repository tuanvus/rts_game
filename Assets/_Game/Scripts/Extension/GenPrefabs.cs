using System.Collections;
using System.Collections.Generic;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine;

public class GenPrefabs : MonoBehaviour
{
    [SerializeField] private GameObject prefabBase;

    [SerializeField] private List<GameObject> prefab;

    void Start()
    {
    }

    [Button]
    public void Gen()
    {
        foreach (var p in prefab)
        {
            var obj = Instantiate(prefabBase);
            obj.gameObject.name = p.name;
            obj.transform.SetParent(transform);
            var tf = obj.transform.Find("Model");
            var objSub = Instantiate(p);
            objSub.transform.SetParent(tf);
            objSub.transform.localPosition = Vector3.zero;
        }
    }
}