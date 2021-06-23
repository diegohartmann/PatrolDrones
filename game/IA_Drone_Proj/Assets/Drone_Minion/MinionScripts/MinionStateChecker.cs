using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinionStates{
    Locked,
    Stoped,
    Follow,
}
public class MinionStateChecker : MonoBehaviour
{
    [SerializeField]private MinionStates State = MinionStates.Locked;
    private PlayerMovement PlayerMov = null;
    private MinionComponents components;
    private GameObject thisMinion;

    public void StateCheckerInit(MinionComponents comp) {
        components = comp;
        PlayerMov = Player().gameObject.GetComponent<PlayerMovement>();
        thisMinion = this.gameObject;
    }
    public void MinionStateMachine() {
         if(Player() == null){
            State = MinionStates.Stoped;
            return;
        }
        ExecuteState();
    }
    private float DistFrom(Transform target){
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
        p("locked");
    }

    private void Stoped(){
        p("Stoped");
    }

    private Transform Player(){
        return components.player;
    }
    private void FollowMachine(){
        if(DistFrom(Player()) < 2){
            StandOnPlayer();
            return;
        }
        MoveToPlayer();
    }
    private void MoveToPlayer(){
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
        p("standOnPlayer");
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
    void p (string _string){
        //print(_string);
    }
}
