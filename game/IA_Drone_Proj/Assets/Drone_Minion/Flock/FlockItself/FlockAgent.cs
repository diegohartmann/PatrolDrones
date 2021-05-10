using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    [SerializeField]int flockGroup = 0;
    [HideInInspector]public int FlockGroup {get{return flockGroup;}}
    
    Collider agentCollider;
    Flock flock;
    [HideInInspector]public Collider AgentCollider {get {return agentCollider;}}
    void Awake(){
        flock = FindObjectOfType<Flock>();
        agentCollider = GetComponent<Collider>();        
    }
    
    public void MoveFlockAgent(){
        Move(MovementDir());
    }
    private void Move(Vector3 _velocity){
        transform.forward = _velocity;
        transform.position += _velocity * Time.deltaTime;
    }
    private Vector3 MovementDir(){
        List<Transform> context = GetNearbyObjects();
        Vector3 move = flock.behaviour.CalculateMove(this, context, flock);
        move *= flock.driveFactor;
        if(move.sqrMagnitude > flock.squareOfMaxSpeed){
            move = (move.normalized * flock.maxSpeed);
        }
        return (new Vector3(move.x, 0, move.z));
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
