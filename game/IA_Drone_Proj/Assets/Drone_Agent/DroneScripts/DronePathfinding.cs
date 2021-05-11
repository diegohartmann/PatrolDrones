using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto used for this script: https://www.youtube.com/watch?v=dn1XRIaROM4&ab_channel=SebastianLague
public class DronePathfinding : MonoBehaviour
{
    [HideInInspector]public Vector3 currentTargetWaypoint;
    [HideInInspector]public Vector3[] path; //taken from grid
    // [HideInInspector]public Vector3 LastFinalTarget;
    [HideInInspector]public bool startFollow = false;
    [HideInInspector]public bool canRequestAPath = true;

    private int targetIndex;
    
    public void RequestAPath(Vector3 _finalTarget){
        PathRequestManager.RequestPath(transform.position, _finalTarget, OnPathFound);
        targetIndex = 0;
        canRequestAPath = false;
    }
    private void OnPathFound ( Vector3[] newPath, bool pathSuccessful){
        if (pathSuccessful){
            path = newPath;
            startFollow = true;
            return;
        }
        print("não conseguiu criar caminho");
    }

    public void UpdatePathfindingWay() {
        if(startFollow && (targetIndex < path.Length)){
            if (DistanceFrom(targetIndex) < 1f)
            {    
                targetIndex ++;
                if (targetIndex == path.Length){
                    startFollow = false;
                    canRequestAPath = true;
                    targetIndex = 0;
                    return;
                }
                currentTargetWaypoint = path[targetIndex];
            }
            return;
        }
        canRequestAPath = true;
        targetIndex = 0; 
        startFollow = false;
    }

    private float DistanceFrom(int _index){
        return Vector3.Distance(transform.position, path[_index]);
    }

    private void OnDrawGizmos() {
        if (path != null){
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
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
