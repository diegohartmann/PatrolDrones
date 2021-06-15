using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tuto: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
public abstract class FlockBehaviour : ScriptableObject
{
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
