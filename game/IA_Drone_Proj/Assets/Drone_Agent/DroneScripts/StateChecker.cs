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

public class StateChecker : MonoBehaviour
{  
    private DroneStates State =  DroneStates.Patrol;  
    public void SetState(DroneComponents comp){
        if (HasATarget(comp.fieldOfView))
        {
            RefillSearchTimer(comp.status); 
            SeesTarget(true);
            if (ReachedAnyPatrolPoint(comp.patrol)){
                ReachedAnyPatrolPoint(false, comp.patrol);
            }
            if(!DroneCanRequestAPathAStar(comp.aStar)){
                DroneCanRequestAPathAStar(true, comp.aStar);
            }
            if (DistFromTarget_GraterThan(comp.status.distanceToAttack, comp.fieldOfView)){
                State = DroneStates.Chase;
                return;
            }
            State = DroneStates.Attack;
            return;
        }
        SeesTarget(false);
        if (AnotherDroneHasATarget()){
            RefillSearchTimer(comp.status);
            State = DroneStates.Search;
            return;
        }
        if (IsTimeToSearch(comp.status)){
            State = DroneStates.Search;
            comp.status.searchTimer -= Time.deltaTime;
            return;
        }
        if (!ReachedAnyPatrolPoint(comp.patrol)){
            State = DroneStates.BackToPatrol;
            return;
        }
        if(DroneCanRequestAPathAStar(comp.aStar)){
            DroneCanRequestAPathAStar(false, comp.aStar);
        }
        State = DroneStates.Patrol;
    }

    public void ExecuteState(StateActions actions){
        switch (State)
        {
            case DroneStates.Attack:
                actions.Attack(); 
            break;

            case DroneStates.Chase:
                actions.Chase();
            break;

            case DroneStates.Search:
                actions.Search(); 
            break;

            case DroneStates.BackToPatrol:
                actions.GoingBackToPatrol(); 
            break;

            case DroneStates.Patrol:
                actions.Patrol();
            break;
            
            default:
            break;
        }
    }
    private bool DistFromTarget_GraterThan(float _amount, FieldOfView fieldOfView){
        return (Vector3.Distance(transform.position, fieldOfView.ClosestTarget.position) > _amount);
    }
    private bool HasATarget(FieldOfView fieldOfView){
        return fieldOfView.ClosestTarget != null;
    }
    private bool ReachedAnyPatrolPoint(PatrolWaypoints patrol){
        return patrol.reachedPatrol;
    }
    private void ReachedAnyPatrolPoint(bool _b, PatrolWaypoints patrol){
        patrol.reachedPatrol = _b;
    }
    private bool AnotherDroneHasATarget(){
        return DronesNetworkComunication.dronesViewingIntruser.Count>0;
    }
    private void DroneCanRequestAPathAStar(bool b, DronePathfinding aStar){
        aStar.canRequestAPath = b;
    }
    private bool DroneCanRequestAPathAStar(DronePathfinding aStar){
        return aStar.canRequestAPath;
    }
    private void RefillSearchTimer(DroneStatus status){
        if (status.searchTimer < DroneStatus.maxSearchTimerValue)
            status.searchTimer = DroneStatus.maxSearchTimerValue;
    }
    private bool IsTimeToSearch(DroneStatus status){
        return status.searchTimer > DroneStatus.minSearchTimerValue;
    }
    private void SeesTarget(bool sees){
        if (sees){
            if (!DronesNetworkComunication.dronesViewingIntruser.Contains(this.gameObject))
            {
                DronesNetworkComunication.dronesViewingIntruser.Add(this.gameObject);
            }
            return;
        }
        if (DronesNetworkComunication.dronesViewingIntruser.Contains(this.gameObject))
        {
            DronesNetworkComunication.dronesViewingIntruser.Remove(this.gameObject);
        }
    }
}
