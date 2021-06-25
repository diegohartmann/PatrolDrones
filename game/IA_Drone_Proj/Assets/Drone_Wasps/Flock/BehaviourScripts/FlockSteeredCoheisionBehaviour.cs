using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto: https://www.youtube.com/watch?v=qzQZl09HDmI&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=5&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCoheision")]
public class FlockSteeredCoheisionBehaviour : FilteredFlockBehaviour
{
    private Vector3 currentVelocity;
    [SerializeField] private float agentSmoothTime = 0.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        if(context.Count == 0){
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext){
            cohesionMove += (item.position);
        }
        cohesionMove /= context.Count;
        //offset
        cohesionMove -= (agent.transform.position);
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}