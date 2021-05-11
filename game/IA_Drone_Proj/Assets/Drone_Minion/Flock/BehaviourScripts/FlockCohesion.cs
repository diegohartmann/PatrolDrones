using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto https://www.youtube.com/watch?v=7LDFLMRGyqs&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=3&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class FlockCohesion : FilteredFlockBehaviour
{
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
        //diferença
        cohesionMove -= (agent.transform.position);
        return cohesionMove;
    }
}
