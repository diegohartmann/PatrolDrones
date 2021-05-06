﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto: https://www.youtube.com/watch?v=qzQZl09HDmI&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=5&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCoheision")]
public class FlockSteeredCoheisionBehaviour : FlockBehaviour
{
    Vector2 currentVelocity;
    [SerializeField] float agentSmoothTime = 0.5f;
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
        cohesionMove = Vector2.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
