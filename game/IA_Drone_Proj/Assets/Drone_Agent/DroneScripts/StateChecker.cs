using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public enum DroneStates{
    Patrol,
    Chase,
    Search,
    BackToPatrol, //alterar nome
    Attack,
}

class StateChecker : MonoBehaviour
{  
    private DroneStates State =  DroneStates.BackToPatrol;  
    private DroneComponents comp;
    private GameObject thisDrone;

    private void Start(){
        SetStuff();
    }

    private void SetStuff(){
        comp = GetComponent<DroneComponents>();
        thisDrone = this.gameObject;
        comp.status.searchTimer = DroneStatus.minSearchTimerValue;
    }

    private void Update(){
        LookForTargets();
        SetState();
        ExecuteState();
    }

    private void LateUpdate() {
        comp.fieldOfView.DrawFieldOfView();
    }

    private void LookForTargets(){
        comp.fieldOfView.FindVisibleTargets();
    }
   
    private void SetState(){
        if (HasATarget())
        {
            RefillSearchTimer(); 
            SeesTarget(true);
            if (ReachedAnyPatrolPoint()){
                ReachedAnyPatrolPoint(false);
            }
            if(!DroneCanRequestAPathAStar()){
                DroneCanRequestAPathAStar(true);
            }
            if (DistFromTarget_GraterThan(comp.status.distanceToAttack)){
                State = DroneStates.Chase;
                return;
            }
            State = DroneStates.Attack;
            return;
        }
        SeesTarget(false);
        if (AnotherDroneHasATarget()){
            RefillSearchTimer();
            State = DroneStates.Search;
            return;
        }
        if (IsTimeToSearch()){
            State = DroneStates.Search;
            comp.status.searchTimer -= Time.deltaTime;
            return;
        }
        if (!ReachedAnyPatrolPoint()){
            State = DroneStates.BackToPatrol;
            return;
        }
        if(DroneCanRequestAPathAStar()){
            DroneCanRequestAPathAStar(false);
        }
        State = DroneStates.Patrol;
    }

    private void ExecuteState(){
        switch (State)
        {
            case DroneStates.Attack:
                comp.actions.Attack(); 
            break;

            case DroneStates.Chase:
                comp.actions.Chase();
            break;

            case DroneStates.Search:
                comp.actions.Search(); 
            break;

            case DroneStates.BackToPatrol:
                comp.actions.GoingBackToPatrol(); 
            break;

            case DroneStates.Patrol:
                comp.actions.Patrol();
            break;
            
            default:
            break;
        }
    }

    private bool DistFromTarget_GraterThan(float _amount){
        return (Vector3.Distance(transform.position, comp.fieldOfView.ClosestTarget.position) > _amount);
    }

    private bool HasATarget(){
        return comp.fieldOfView.ClosestTarget != null;
    }

    private bool ReachedAnyPatrolPoint(){
        return comp.patrol.reachedPatrol;
    }
   
    private void ReachedAnyPatrolPoint(bool _b){
        comp.patrol.reachedPatrol = _b;
    }

    private bool AnotherDroneHasATarget(){
        return DronesNetworkComunication.dronesViewingIntruser.Count>0;
    }
    private void DroneCanRequestAPathAStar(bool b){
        comp.aStar.canRequestAPath = b;
    }

    private bool DroneCanRequestAPathAStar(){
        return comp.aStar.canRequestAPath;
    }

    private void RefillSearchTimer(){
        if (comp.status.searchTimer < DroneStatus.maxSearchTimerValue)
            comp.status.searchTimer = DroneStatus.maxSearchTimerValue;
    }

    private bool IsTimeToSearch(){
        return comp.status.searchTimer > DroneStatus.minSearchTimerValue;
    }

    private void SeesTarget(bool sees){
        if (sees){
            if (!DronesNetworkComunication.dronesViewingIntruser.Contains(thisDrone))
            {
                DronesNetworkComunication.dronesViewingIntruser.Add(thisDrone);
            }
            return;
        }
        if (DronesNetworkComunication.dronesViewingIntruser.Contains(thisDrone))
        {
            DronesNetworkComunication.dronesViewingIntruser.Remove(thisDrone);
        }

    }
}
