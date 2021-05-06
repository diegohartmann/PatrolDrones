using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class FlockAvoidance : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        if(context.Count == 0){
            return Vector3.zero;
        }
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
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
