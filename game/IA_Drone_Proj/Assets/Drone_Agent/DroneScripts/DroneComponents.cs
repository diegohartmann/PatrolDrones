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
    [HideInInspector]public StateChecker stateChecker;
    [HideInInspector]public DamageFromBullet damageFromBullets;
    [HideInInspector]public PathRequestManager pathRequest;
    public void ComponentsInit(){
        fieldOfView = GetComponent<FieldOfView>();
        status = GetComponent<DroneStatus>();
        actions = GetComponent<StateActions>();
        bulletsPool = GetComponent<BulletsPool>();
        patrol = GetComponent<PatrolWaypoints>();
        aStar = GetComponent<DronePathfinding>();
        stateChecker = GetComponent<StateChecker>();
        damageFromBullets = GetComponent<DamageFromBullet>();
        pathRequest = GetComponent<PathRequestManager>();
    }
}
