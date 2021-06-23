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
    public void FlockAgentInit(){
        flock = FindObjectOfType<Flock>();
        agentCollider = GetComponent<Collider>();        
    }
    public void MoveFlockAgent(){
        Move(MovementDir());
        RotateToLeader();
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
    private GameObject GetLeaderMinion(){
        return MinionsNetworking.leaderMinion;
    }
    private void RotateTo(Vector3 _target, bool _isSmooth, float _rotSpeed = 1) {
        if (_isSmooth){
            var neededRotation = Quaternion.LookRotation(_target - transform.position);
            var interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * _rotSpeed);
            transform.rotation = interpolatedRotation;
        }
        else{
            transform.LookAt(_target);
        }
    }
    private void RotateToLeader(){
        GameObject leader = GetLeaderMinion();
        if(leader == null){
            return;
        }
        RotateTo(leader.transform.position, true , 5);
    }
}
