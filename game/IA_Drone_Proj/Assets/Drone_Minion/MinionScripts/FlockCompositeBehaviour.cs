using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto: https://www.youtube.com/watch?v=o9JdUYsPkzg&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=4&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class FlockCompositeBehaviour : FlockBehaviour
{
    [SerializeField] FlockBehaviour[] behaviours;
    [SerializeField] float[] weights;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock){
        //sem match
        if(weights.Length != behaviours.Length){
            Debug.LogError("Data Mismatch in" + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;
        
        for (int i = 0; i < behaviours.Length; i++){
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];
            if(partialMove != Vector2.zero){
                if(partialMove.sqrMagnitude > weights[i] * weights[i]){
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }
        return move;
    }
}
