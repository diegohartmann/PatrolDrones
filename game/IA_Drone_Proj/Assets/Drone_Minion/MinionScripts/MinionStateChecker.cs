using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinionStates{
    Locked,
    Stoped,
    StandOnPlayer,
    Follow,
    GoingToFixedArea,
}
public class MinionStateChecker : MonoBehaviour
{
    public bool tempLeader = false;
    [SerializeField]private  float distToStandOnPlayer = 1.88f;
    private const float distToSimpleFollow = 3f;
    [SerializeField]private MinionStates State = MinionStates.Locked;
    private exempleMove PlayerMov = null;
    private MinionComponents components;
    private FlockAgent thisFlockAgent;
    private Flock flock;

    private void Awake() {
        thisFlockAgent = GetComponent<FlockAgent>();
        flock = FindObjectOfType<Flock>();
        components = GetComponent<MinionComponents>();
    }
    private void Start() {
        PlayerMov = Player().gameObject.GetComponent<exempleMove>();
        if(tempLeader){
            MinionsNetworking.leaderMinion = this.gameObject;
        }
    }
    private void Update() {
       SetState();
       ExecuteState();
    }
    private Transform Player(){
        return components.player;
    }
    private Vector3 PlayerPos(){
        return Player().position;
    }
    public float DistFromPlayer(){
        return Vector3.Distance(transform.position, PlayerPos());
    }

    private void SetState(){
        // if(components.status.isLocked){
        //     State = MinionStates.Locked;
        //     return;
        // }
        // if(components.status.isGoingToFixedArea){
        //     State = MinionStates.GoingToFixedArea;
        //     return;
        // }
        if(Player() == null){
            State = MinionStates.Stoped;
            return;
        }
      
        if(DistFromPlayer() > distToStandOnPlayer){ //e se o minion ta longe do player
            State = MinionStates.Follow; //ele segue o player
            return;
        }
        State = MinionStates.StandOnPlayer; //se nao, para do lado do player
    }

    private void ExecuteState(){
        switch (State)
        {
            case MinionStates.Locked:
                Locked();
            break;
            
            case MinionStates.Follow:
                FollowMachine();
            break;

            case MinionStates.Stoped:
                Stoped();
            break;

            case MinionStates.StandOnPlayer:
                StandOnPlayer();
            break;

            default:
            break;
        }
    }

    private void Locked(){
        //print ("locked");
    }

    private void Stoped(){
        //print("Stoped");
    }

    private void GoTo(){
        // components.GoToArea();
        //print("Going To Some Area");
    }
    private void FollowMachine(){
        if(MinionsNetworking.leaderMinion == this.gameObject){
            // flock.agents.Remove(thisFlockAgent);
            if(PlayerMov.isOnTile){ //se player ta no tile (grid)
                if(DistFromPlayer() > distToSimpleFollow){
                    components.actions.AStartToPlayer();
                    return;
                }
                if(DistFromPlayer() > distToSimpleFollow / 2){
                    components.actions.SimpleFollowPlayer();
                    return;
                }
                return;
            }
            components.actions.SimpleFollowPlayer();// se nao, só segue o player sem pathfinding
            return;
        }
        components.actions.Flocking();// comportamento de flocking, seguindo lider
       
    }

    private void StandOnPlayer(){
        //print("standOnPlayer");
    }
}
