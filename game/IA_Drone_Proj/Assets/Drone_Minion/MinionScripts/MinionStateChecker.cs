using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinionStates{
    Locked,
    Stoped,
    Follow,
    GoingToFixedArea,
}
public class MinionStateChecker : MonoBehaviour
{
    public bool instaFlock;
    // public bool tempLeader = false;
    [SerializeField]private MinionStates State = MinionStates.Locked;
    private exempleMove PlayerMov = null;
    private MinionComponents components;
    private GameObject thisMinion;

    private void Start() {
        components = GetComponent<MinionComponents>();
        PlayerMov = Player().gameObject.GetComponent<exempleMove>();
        thisMinion = this.gameObject;
    }
        
    private Transform Player(){
        return components.player;
    }
    private void Update() {
        if(instaFlock){
            FlockMovement();
            return;
        }
         if(Player() == null){
            State = MinionStates.Stoped;
            return;
        }
        // SetState();
        ExecuteState();
    }
    public float DistFrom(Transform target){
        return Vector3.Distance(transform.position, target.position);
    }
    private void SetState(){
        if(Player() == null){
            State = MinionStates.Stoped;
            return;
        }
        State = MinionStates.Follow;
        return;
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
        if(GetLeaderMinion() == thisMinion){
            LeaderMovement();
            return;
        }
        FlockMovement();
    }
    private void FlockMovement(){
        components.actions.Flocking();
    }
    private void LeaderMovement(){
        if(DistFrom(Player()) < 2){
            StandOnPlayer();
            return;
        }
        if(PlayerMov.isOnTile){
            FollowDistanceChecker();
            return;
        }
        components.actions.SimpleFollowPlayer();
    }
    private void FollowDistanceChecker(){
        if(DistFrom(Player()) < 3f){
            components.actions.SimpleFollowPlayer();
            return;
        }
        components.actions.AStartToPlayer();
    }
    private void StandOnPlayer(){
        print("standOnPlayer");
    }
    public void TryToSetAsLeaderMinion(){
        if(GetLeaderMinion() == null){
            SetLeaderMinion(thisMinion);
        }
    }

    private GameObject GetLeaderMinion(){
        return MinionsNetworking.leaderMinion;
    }
    private void SetLeaderMinion(GameObject _minion){
        MinionsNetworking.leaderMinion = _minion;
    }
}
