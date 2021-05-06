using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    Flock flock;
    [HideInInspector]public Collider AgentCollider {get {return agentCollider;}}
    void Awake(){
        flock = FindObjectOfType<Flock>();
        agentCollider = GetComponent<Collider>();        
    }
    public void MoveFlockAgent(Transform target){
        Move(MovementDir(target));
    }
    private void Move(Vector2 _velocity){
        transform.forward = _velocity;
        transform.position += (Vector3)_velocity * Time.deltaTime;
    }
    private Vector2 MovementDir(Transform target){
        List<Transform> context = GetNearbyObjects();
        Vector2 move = flock.behaviour.CalculateMove(this, context, flock, target);
        move *= flock.driveFactor;
        if(move.sqrMagnitude > flock.squareOfMaxSpeed){
            move = (move.normalized * flock.maxSpeed);
        }
        return move;
    }

    private List<Transform> GetNearbyObjects(){
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(transform.position, flock.neighborRadius);
        foreach (Collider c in contextColliders){
            if(c != this.agentCollider){
                context.Add(c.transform);
            }
        }
        return context;
    }

}
