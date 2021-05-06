using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tuto: https://www.youtube.com/watch?v=o9JdUYsPkzg&list=RDCMUCifiUB82IZ6kCkjNXN8dwsQ&index=4&ab_channel=BoardToBitsGames
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class FlockCompositeBehaviour : FlockBehaviour
{
    [SerializeField] FlockBehaviour[] behaviors = null;
    [SerializeField] float[] weights = null;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //handle data mismatch
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //set up move
        Vector2 move = Vector2.zero;

        //iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;

            }
        }

        return move;


    }
}
