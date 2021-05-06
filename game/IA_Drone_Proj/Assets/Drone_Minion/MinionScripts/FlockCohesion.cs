using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto https://www.youtube.com/watch?v=7LDFLMRGyqs&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=3&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class FlockCohesion : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        if(context.Count == 0){
            return Vector2.zero;
        }
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context){
            cohesionMove += (Vector2)(item.position);
        }
        cohesionMove /= context.Count;
        //offset
        cohesionMove -= (Vector2)(agent.transform.position);
        return cohesionMove;
    }
}
