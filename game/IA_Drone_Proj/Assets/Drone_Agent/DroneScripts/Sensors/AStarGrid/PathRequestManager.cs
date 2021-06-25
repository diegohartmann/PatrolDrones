using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto used to make this script: https://www.youtube.com/watch?v=dn1XRIaROM4&ab_channel=SebastianLague
public class PathRequestManager : MonoBehaviour
{
    //stores the requests
    Queue<PathRequest> pathRequestQueue = new Queue <PathRequest>();
    PathRequest currentPathRequest;

    private static PathRequestManager instance;

    [SerializeField] private AStartPathfinding pathfinding = null;
    bool isProcessingPath;

    public void Init(AStartPathfinding _aStar) {
        instance = this;
        pathfinding = _aStar;
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback){
        PathRequest newRequest = new PathRequest (pathStart, pathEnd, callback);
        if(instance != null){
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }
    }
    private void TryProcessNext() {
		if (!isProcessingPath && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
            if(pathfinding == null){
                pathfinding = GetComponent<AStartPathfinding>();
            }
			pathfinding.StartFindingPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}
    public void FinishedProcessingPath(Vector3[] path, bool success) {
		currentPathRequest.callback(path,success);
		isProcessingPath = false;
		TryProcessNext();
	}
   struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}

	}
}
