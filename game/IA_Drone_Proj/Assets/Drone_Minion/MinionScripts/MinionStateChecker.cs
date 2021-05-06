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
    private Transform Player;
    private MinionComponents components;
    private FlockAgent thisFlockAgent;
    private Flock flock;

    private void Awake() {
        thisFlockAgent = GetComponent<FlockAgent>();
        flock = FindObjectOfType<Flock>();
        components = GetComponent<MinionComponents>();
    }
    private void Start() {
        Player = components.player;
        PlayerMov = Player.gameObject.GetComponent<exempleMove>();
        if(tempLeader){
            MinionsNetworking.leaderMinion = this.gameObject;
        }
    }
    private void Update() {
        // SetState();
        ExecuteState();
    }
    public float DistFrom(Transform target){
        return Vector3.Distance(transform.position, target.position);
    }
    private void SetState(){
        if(Player == null){
            State = MinionStates.Stoped;
            return;
        }
        if(DistFrom(Player) > distToStandOnPlayer){ //e se o minion ta longe do player
            State = MinionStates.Follow; //ele segue o player
            return;
        }
        State = MinionStates.StandOnPlayer; //se nao, para do lado do player
    }

    public void SetState(int _state){
        switch (_state)
        {
            case 0:
                State = MinionStates.Locked;
            break;
            
            case 1:
                State = MinionStates.Stoped;
            break;

            case 2:
                State = MinionStates.StandOnPlayer;
            break;

            case 3:
                State = MinionStates.Follow;
            break;

            default:
                Debug.LogWarning("não há estado para esse número");
            break;
        }   
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
                if(DistFrom(Player) > distToSimpleFollow){
                    components.actions.AStartToPlayer();
                    return;
                }
                if(DistFrom(Player) > distToSimpleFollow / 2){
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
