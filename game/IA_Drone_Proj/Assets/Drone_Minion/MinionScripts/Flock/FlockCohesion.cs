using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto https://www.youtube.com/watch?v=7LDFLMRGyqs&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=3&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class FlockCohesion : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        if(context.Count == 0){
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in context){
            cohesionMove += (item.position);
        }
        cohesionMove /= context.Count;
        //offset
        cohesionMove -= (agent.transform.position);
        return cohesionMove;
    }
}
