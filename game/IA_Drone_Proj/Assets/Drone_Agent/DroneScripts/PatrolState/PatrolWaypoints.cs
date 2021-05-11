using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class PatrolWaypoints : MonoBehaviour
{
    private DroneStatus status;
    [HideInInspector]public Transform targetWaypoint;
    [SerializeField] private Transform _PatrolWaypoints = null;
    // [SerializeField] private bool smartSearch = true; 
    [SerializeField][Range(-1, 1)] private int direction = 1;
    [SerializeField][Range(0.0f, 2.0f)] private float minDistToWaypoint = 1.0f;
    private int targetIndex = 0;
    private int lastTargetIndex = -1;
    [HideInInspector] public bool reachedPatrol = false;
    private void Awake() {
        targetWaypoint = _PatrolWaypoints.GetChild(0);
    }
    
    public void SmartWaypoints(){
        for(int i = 0; i < _PatrolWaypoints.childCount; i++){
            SetNextWaypointBasedOn(i);
        } 
    }

    public void SimpleWaypoints(){
        SetNextWaypointBasedOn(targetIndex);
    }

    private void SetNextWaypointBasedOn(int _index){
        if (DistanceFrom(_index) < minDistToWaypoint){
            SetBoolFlag();
            TargetIndexEqualsTo(_index + direction);
        }
    }
    
    private void TargetIndexEqualsTo(int _newTargetIndex){
        targetIndex = _newTargetIndex;
        if (targetIndex == _PatrolWaypoints.childCount){
            targetIndex = 0;
        }
        else if(targetIndex < 0){
            targetIndex  = _PatrolWaypoints.childCount -1;
        }
        UpdateTargetWaypoint();
    }

    private void SetBoolFlag(){
        if (!reachedPatrol){
            reachedPatrol = true;
        }
    }

    private float DistanceFrom(int _index){
        return Vector3.Distance(transform.position, _PatrolWaypoints.GetChild(_index).position);
    }

    private void UpdateTargetWaypoint(){
        //for performance --> only asign the new target waypoint if it is different from the last one reached
        if (targetIndex != lastTargetIndex) 
        {
            targetWaypoint = _PatrolWaypoints.GetChild(targetIndex);
            lastTargetIndex = targetIndex; // --> says that the last one reached is the this new target so it only updates after the target changes again
        }
    }
}
