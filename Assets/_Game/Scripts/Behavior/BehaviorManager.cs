using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorManager : MonoBehaviour
{
    public List<EventCoordinator> eventCoordinators;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class EventCoordinator
{
    public TypeBehavior behaviorCurrent;
    public StateHandler behaviorHandler;
    public TypeBehavior behaviorNext;

}

public enum TypeBehavior
{
    IDLE,
    ATTACK,
    BUILDING,
    MOVEMENT,
    PRODUCING_UNITS,
    REPAIRING,
    RESOURCE_GATHERING,
    TRANSPORTING
}