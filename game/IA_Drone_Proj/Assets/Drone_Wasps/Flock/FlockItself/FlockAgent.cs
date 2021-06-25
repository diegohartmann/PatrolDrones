using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class FlockAgent : MonoBehaviour
{
    private int flockGroup = 0;
    public int FlockGroup {get{return flockGroup;}}
    private Collider agentCollider;
    private Flock flock;
    public Collider AgentCollider {get {return agentCollider;}}
    public void Init (Flock _flock, Collider _col){
        flock = _flock;
        agentCollider = _col;        
    }
    public void MoveFlockAgent(){
        Move(MovementDir());
    }
    private void Move(Vector3 _direction){
        transform.forward = _direction;
        transform.position += _direction * Time.deltaTime;
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
