using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto: https://www.youtube.com/watch?v=kmwVDSUivkg&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=6
[CreateAssetMenu(menuName = "Flock/Behaviour/StayInRadius")]
public class FlockStayInRadiusBehaviour : FlockBehaviour
{
    [SerializeField] Vector3 center = Vector2.zero;
    [SerializeField] float radius = 15f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        Vector3 centerOffset = (center - agent.transform.position);
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f){
            return Vector3.zero;
        }
        return centerOffset * t * t;
    }
}
