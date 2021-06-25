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
    [HideInInspector]public IAAgentPathFinding aStar;
    [HideInInspector]public StateChecker stateChecker;
    [HideInInspector]public DamageFromBullet damageFromBullets;
    [HideInInspector]public PathRequestManager pathRequest;
    [HideInInspector]public ShooterData shooterData;
    [HideInInspector]public Fire fire;
    public void ComponentsInit(){
        fieldOfView = GetComponent<FieldOfView>();
        status = GetComponent<DroneStatus>();
        actions = GetComponent<StateActions>();
        bulletsPool = GetComponent<BulletsPool>();
        patrol = GetComponent<PatrolWaypoints>();
        stateChecker = GetComponent<StateChecker>();
        damageFromBullets = GetComponent<DamageFromBullet>();
        pathRequest = GetComponent<PathRequestManager>();
        shooterData = GetComponent<ShooterData>();
        fire = new Fire(bulletsPool, shooterData);
        aStar = new IAAgentPathFinding(this.transform);
    }
}
