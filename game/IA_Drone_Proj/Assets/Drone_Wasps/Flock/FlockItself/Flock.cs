using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
public class Flock : MonoBehaviour
{
    public FlockBehaviour behaviour = null;
    const float AgentDensity = 0.08f;
    [Range(1, 100)]public float driveFactor = 10;
    [Range(.1f, 100)]public float maxSpeed = 5f;
    [Range(1, 10)]public float neighborRadius = 1.5f;
    [Range(0, 1)][SerializeField] private float avoidanceRadiusMultiplier = 0.5f;
    [HideInInspector]public float squareOfMaxSpeed;
    [HideInInspector]public float squareOfNeighborRadius;
    [HideInInspector]public float squareOfAvoidanceRadius;
    [HideInInspector] public float SquareOfAvoidanceRadious{get {return squareOfAvoidanceRadius;}}
    
    void Awake(){
        SetSquareStuff();
    }
    void SetSquareStuff(){
        squareOfMaxSpeed = SquareOf(maxSpeed);
        squareOfNeighborRadius = SquareOf(neighborRadius);
        squareOfAvoidanceRadius = SquareOf(avoidanceRadiusMultiplier) * squareOfNeighborRadius;
    }
    private float SquareOf(float _n){
        return _n*_n;
    }
}
