using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public List<NodeResource> nodeResources;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public NodeResource GetNodeResource(Transform resourceNode)
    {
        for (int i = 0; i < nodeResources.Count; i++)
        {
            Debug.Log("dis " + Vector3.Distance(resourceNode.position, nodeResources[i].transform.position));
            if (Vector3.Distance(resourceNode.position , nodeResources[i].transform.position) < 10f && nodeResources[i].Amount > 0)
            {
                return nodeResources[i];
            }
        }
        return null;
    }
}
