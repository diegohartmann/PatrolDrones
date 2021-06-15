using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto: https://www.youtube.com/watch?v=o9JdUYsPkzg&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=4&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class FlockCompositeBehaviour : FlockBehaviour
{
    [SerializeField] FlockBehaviour[] behaviors = null;
    [SerializeField] float[] weights = null;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviors.Length){
            Debug.LogError("numero de behaviours é diferente do numero de pesos, em " + name, this);
            return Vector3.zero;
        }
        //seta movimento
        Vector3 move = Vector3.zero;
        //passa pelos behaviours (align, cohesion, avoidance)
        for (int i = 0; i < behaviors.Length; i++){
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
            if (partialMove != Vector3.zero){
                if (partialMove.sqrMagnitude > weights[i] * weights[i]){
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }
        return move;
    }
}
