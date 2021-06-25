using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto used for this script: https://www.youtube.com/watch?v=dn1XRIaROM4&ab_channel=SebastianLague
public class IAAgentPathFinding
{
    public Vector3 currentTargetWaypoint;
    public Vector3[] path; //taken from grid
    public bool startFollow = false;
    public bool canRequestAPath = true;
    
    private int targetIndex = 0;
    private Transform follower;
    public IAAgentPathFinding(Transform _follower){
        follower =_follower;
    }
    public void RequestAPath(Vector3 _finalTarget){
        PathRequestManager.RequestPath(follower.position, _finalTarget, OnPathFound);
        this.targetIndex = 0;
        this.canRequestAPath = false;
    }
    private void OnPathFound ( Vector3[] newPath, bool pathSuccessful){
        if (pathSuccessful){
            this.path = newPath;
            this.currentTargetWaypoint = this.path[0];
            this.startFollow = true;
            return;
        }
        Debug.LogWarning(follower.gameObject.name + " path failed");
    }

    public void UpdatePathfindingWay() {
        if(this.startFollow && (this.targetIndex < this.path.Length)){
            if (DistanceFrom(this.targetIndex) < 1f){    
                this.targetIndex ++;
                if (this.targetIndex >= this.path.Length){
                    this.startFollow = false;
                    this.canRequestAPath = true;
                    this.targetIndex = 0;
                    this.path = new Vector3[0];
                    return;
                }
                this.currentTargetWaypoint = this.path[targetIndex];
            }
            return;
        }
        this.canRequestAPath = true;
        this.targetIndex = 0; 
        this.startFollow = false;
    }

    private float DistanceFrom(int _index){
        return Vector3.Distance(follower.position, path[_index]);
    }

    private void OnDrawGizmos() {
        if (path != null){
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex){
                    Gizmos.DrawLine(follower.position, path[i]);
                }
                else{
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
