using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MinionStates{
    Locked,
    Wait,
    Follow,
    GoingToFixedArea,
}
public class MinionStateChecker : MonoBehaviour
{
    private const float distToFollow = 4f;
    private const float distToSimpleFollow = 3f;
    [SerializeField]private MinionStates State = MinionStates.Locked;
    private MinionComponents components;

    private void Awake() {
        components = GetComponent<MinionComponents>();
    }
    private void Update() {
    //    SetState();
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
        
        if(components.status.isLocked){
            State = MinionStates.Locked;
            return;
        }
        
        if(components.status.isGoingToFixedArea){
            State = MinionStates.GoingToFixedArea;
            return;
        }
        
        if(components.status.hasToWait){
            State = MinionStates.Wait;
            return;
        }
        if(Player()!= null){
            if(DistFromPlayer() > distToFollow){
                State = MinionStates.Follow;
                return;
            }
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

            case MinionStates.Wait:
                Wait();
            break;

            default:
            break;
        }
    }

    private void Locked(){
        print ("locked");
    }

    private void Wait(){
        print("Waiting");
    }

    private void GoTo(){
        // components.GoToArea();
        print("Going To Some Area");
    }
    private void FollowMachine(){
        print("Following Player");
        if(Player() == null){
            return;
        }
        if(DistFromPlayer() > distToSimpleFollow){
            components.actions.AStartToPlayer();
            return;
        }
        if(DistFromPlayer() > distToSimpleFollow / 2){
            components.actions.SimpleFollowPlayer();
            return;
        }
    }
}
