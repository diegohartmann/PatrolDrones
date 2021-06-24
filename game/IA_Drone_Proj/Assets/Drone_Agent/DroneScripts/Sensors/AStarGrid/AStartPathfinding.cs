using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class AStartPathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    private Grid grid;

    private void Awake() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }
    public void StartFindingPath(Vector3 startPos, Vector3 targetPos){
        StartCoroutine(FindPath(startPos, targetPos));
    }

    private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);
		
		
		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour)){
							openSet.Add(neighbour);
						}
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
	}

    Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

        //EXTRA ADDED ------
        // if (currentNode == startNode){
        //     path.Add(currentNode);
		// }
        //----------------

		//only creates waypoints where the path changes direction (nodes)
        Vector3[] _waypoints = SimplifyPath(path, startNode);
        Array.Reverse(_waypoints);
        return _waypoints;
	}

    private Vector3[] SimplifyPath(List<Node> path, Node startNode){
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2 (path[i-1].gridX - path[i].gridX,  path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld){
                // waypoints.Add(path[i].worldPosition);
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;

            if (i == path.Count - 1 && directionOld != new Vector2(path[i].gridX, path[i].gridY) - new Vector2(startNode.gridX, startNode.gridY))
                waypoints.Add(path[path.Count-1].worldPosition); 
        }

        return waypoints.ToArray();
    }

    private int GetDistance(Node nodeA, Node nodeB){
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return ( (14*dstY) + ( 10 * ( dstX - dstY) ) );
        else
            return ( (14*dstX) + ( 10 * ( dstY - dstX) ) );
    }
}
