using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPathfinding : MonoBehaviour
{
    [HideInInspector]public Vector3 currentTargetWaypoint;
    [HideInInspector]public Vector3[] path; //taken from grid
    [HideInInspector]public Vector3 LastFinalTarget;
    private int targetIndex;
    private int lastTargetIndex = -1;
    // [HideInInspector] public bool reachedLastPoint ;
    [HideInInspector] public bool startFollow = false;
    [HideInInspector] public bool canRequestAPath = true;

    public void RequestAPath(Vector3 _finalTarget){
        targetIndex = 0; 
        PathRequestManager.RequestPath(transform.position, _finalTarget, OnPathFound);
        LastFinalTarget = _finalTarget;
        // reachedLastPoint = false;
        canRequestAPath = false;
    }

    
    Vector3 player;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void OnPathFound ( Vector3[] newPath, bool pathSuccessful){
        //tuto used for this method: https://www.youtube.com/watch?v=dn1XRIaROM4&ab_channel=SebastianLague
        if (pathSuccessful){
            path = newPath;
            startFollow = true;
            currentTargetWaypoint = path[0];
            return;
        }
        print("confuso, sem caminho (???)");
    }


    public void UpdatePathWaypoints(){
        if (startFollow && path!=null && path.Length > 0){
            if (DistanceFrom(targetIndex) < 0.4f)
            {    
                TargetIndexEqualsTo(targetIndex+1);
            }
            if(FinishCheck()){
                return;
            }
            if (targetIndex != lastTargetIndex) 
            {
                currentTargetWaypoint = path[targetIndex];
                lastTargetIndex = targetIndex; // --> says that the last one reached is the this new target so it only updates after the target changes again
            }
        }
    }

    private float DistanceFrom(int _index){
        return Vector3.Distance(transform.position, path[_index]);
    }
    // private bool reachedCurrTarget(int _index){
    //     return (transform.position == path[_index]);
    // }
    private void TargetIndexEqualsTo(int _amt){
        targetIndex = _amt;
    }
    private bool FinishCheck(){
        if (targetIndex == path.Length){
            // canRequestAPath = true;
            startFollow = false;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos() {
        if (path != null){
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex){
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else{
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
