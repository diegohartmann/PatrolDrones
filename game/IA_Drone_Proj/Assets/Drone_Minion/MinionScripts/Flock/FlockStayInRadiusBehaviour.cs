using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto: https://www.youtube.com/watch?v=kmwVDSUivkg&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=6
[CreateAssetMenu(menuName = "Flock/Behaviour/StayInRadius")]
public class FlockStayInRadiusBehaviour : FlockBehaviour
{
    [SerializeField] Vector2 center = Vector2.zero;
    [SerializeField] float radius = 15f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        Vector2 centerOffset = (center - (Vector2)agent.transform.position);
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f){
            return Vector2.zero;
        }
        return centerOffset * t * t;
    }
}
