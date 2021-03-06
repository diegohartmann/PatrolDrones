using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class FlockAvoidance : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        if(context.Count == 0){
            return Vector3.zero;
        }
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext){
            float magnitude = Vector3.SqrMagnitude(item.position - agent.transform.position);
            if(magnitude < flock.SquareOfAvoidanceRadious){
                nAvoid ++;
                avoidanceMove += (agent.transform.position - item.position);
            }
        }
        if(nAvoid > 0){
            avoidanceMove /= nAvoid;
        }
        return avoidanceMove;
    }
}
