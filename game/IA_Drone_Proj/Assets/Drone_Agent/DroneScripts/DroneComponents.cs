using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class was developed without tuto

public class DroneComponents : MonoBehaviour
{
    [HideInInspector]public FieldOfView fieldOfView;
    [HideInInspector]public DroneStatus status;
    [HideInInspector]public StateActions actions;
    [HideInInspector]public BulletsPool bulletsPool;
    [HideInInspector]public PatrolWaypoints patrol;
    [HideInInspector]public DronePathfinding aStar;
   

    private void Awake(){
        fieldOfView = GetComponent<FieldOfView>();
        status = GetComponent<DroneStatus>();
        actions = GetComponent<StateActions>();
        bulletsPool = GetComponent<BulletsPool>();
        patrol = GetComponent<PatrolWaypoints>();
        aStar = GetComponent<DronePathfinding>();
    }
}
