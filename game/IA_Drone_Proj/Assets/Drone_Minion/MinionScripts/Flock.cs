using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tuto: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
public class Flock : MonoBehaviour
{
    [SerializeField] FlockAgent agentPrefab = null;
    List<FlockAgent> agents = new List<FlockAgent>();
    [SerializeField] FlockBehaviour behaviour = null;
    [Range(10, 500)] [SerializeField] int startCount = 250;
    const float AgentDensity = 0.08f;
    [Range(1, 100)][SerializeField] float driveFactor = 10;
    [Range(1, 100)][SerializeField] float maxSpeed = 5f;
    [Range(1, 10)][SerializeField] float neighborRadius = 1.5f;
    [Range(0, 1)][SerializeField] float avoidanceRadiusMultiplier = 0.5f;
    float squareOfMaxSpeed;
    float squareOfNeighborRadius;
    float squareOfAvoidanceRadius;
    [HideInInspector] public float SquareOfAvoidanceRadious{get {return squareOfAvoidanceRadius;}}
    
    void Awake()
    {
        squareOfMaxSpeed = SquareOf(maxSpeed);
        squareOfNeighborRadius = SquareOf(neighborRadius);
        squareOfAvoidanceRadius = SquareOf(avoidanceRadiusMultiplier) * squareOfNeighborRadius;
        for (int i = 0; i < startCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startCount * AgentDensity,
                Quaternion.Euler( Vector3.forward * Random.Range(0,360)),
                this.transform
            );
            newAgent.gameObject.name = "Agent " + i;
            agents.Add(newAgent);
        }
        transform.Rotate(90,0,0);
    }

    // Update is called once per frame
    private void Update()
    {
        FlockIteration();
    }

    public void FlockIteration(){
        foreach (FlockAgent agent in agents){
            MoveAgent(agent);
        }
    }
    private void MoveAgent(FlockAgent agent){
        List<Transform> context = GetNearbyObjects(agent);
        Vector2 move = behaviour.CalculateMove(agent, context, this);
        move *= driveFactor;
        if(move.sqrMagnitude > squareOfMaxSpeed){
            move = move.normalized * maxSpeed;
        }
        agent.Move(move);
    }
    private List<Transform> GetNearbyObjects(FlockAgent agent){
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders){
            if(c != agent.AgentCollider){
                context.Add(c.transform);
            }
        }
        return context;
    }

    private float SquareOf(float _n){
        return _n*_n;
    }
}
