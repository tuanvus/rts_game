using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS.Resource
{
    public class ResourceNode : MonoBehaviour
    {
        public Event onHit{get; set; }
        public ResourcesType resourcesType;
        public int amount;
        public float distanceTargetStop;
        [SerializeField] float spawnTime;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
