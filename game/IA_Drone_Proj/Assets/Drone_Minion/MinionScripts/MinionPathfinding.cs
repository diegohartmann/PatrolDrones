using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPathfinding : MonoBehaviour
{
    [HideInInspector]public Vector3 currentTargetWaypoint;
    [HideInInspector]public Vector3[] path; //taken from grid
    [HideInInspector]public Vector3 LastFinalTarget;
    private int targetIndex;
    // private int lastTargetIndex = -1;
    public bool reachedTargetTransform;
    public bool pathSuccedded;
    // [HideInInspector] public bool canRequestAPath = true;

    public void RequestAPath(Vector3 _finalTarget){
        targetIndex = 0; 
        PathRequestManager.RequestPath(transform.position, _finalTarget, OnPathFound);
        LastFinalTarget = _finalTarget;
        // canRequestAPath = false;
    }

    private void OnPathFound ( Vector3[] newPath, bool pathSuccessful){
        //tuto used for this method: https://www.youtube.com/watch?v=dn1XRIaROM4&ab_channel=SebastianLague
        this.pathSuccedded = pathSuccessful;
        if (pathSuccessful){
            reachedTargetTransform = false;
            this.path = newPath;
            this.currentTargetWaypoint = path[0];
            return;
        }
        Debug.LogWarning("path failed");
    }


    public void UpdatePathWaypoints(){
        if(!pathSuccedded){
            return;
        }
        if (PathNull()){
            print("PathNull()");
            return;
        }
        if(EmptyPath()){
            return;
        }
        if(!ReachedTargetTransform()){ //enquanto nao chegou no ultimo waypoint...
            UpdateTargetWaypointByDistance(); //o waypoint é atualizado
            return;
        }
        pathSuccedded = false;
        reachedTargetTransform = true;
        // print("Chegou no ultimo waypoint do aStart");
    }
    private void UpdateTargetWaypointByDistance(){
        if (DistanceFrom(this.targetIndex) < 0.4f)
        {    
            this.targetIndex += 1;
            this.currentTargetWaypoint = path[this.targetIndex];
        }
    }
    private bool PathNull(){
        return (this.path == null);
    }
    private bool ReachedTargetTransform(){
        return (this.targetIndex == this.path.Length);
    }
    private bool EmptyPath(){
        return this.path.Length == 0;
    } 
    private float DistanceFrom(int _index){
        return Vector3.Distance(transform.position, path[_index]);
    }
    
    private void OnDrawGizmos() {
        if (this.path != null){
            for (int i = targetIndex; i < this.path.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex){
                    Gizmos.DrawLine(this.transform.position, path[i]);
                }
                else{
                    Gizmos.DrawLine(this.path[i-1], this.path[i]);
                }
            }
        }
    }
}
