using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class StateActions : MonoBehaviour
{
    private DroneComponents comp;
    private float charge = 0;
    private IAAgentMovement movement;
    
    public void ActionsInit(DroneComponents _comp){
      comp = _comp;
      movement = new IAAgentMovement();
    }
    /// --------------------------------------------------------------------------------------------------------//////
    /// -----  ALL METHODS UNDER THIS LINE ARE THE DRONE STATES, CALLED INTO StateChecker's Update() method ----//////
    /// --------------------------------------------------------------------------------------------------------//////
    public void GoingBackToPatrol(){
        this.comp.patrol.UpdateWaypoints(true);
        AStartTo(PositionOf(this.comp.patrol.targetWaypoint));
    }  
   
    public void Patrol(){
        this.comp.patrol.UpdateWaypoints(false);
        movement.GoTo(this.transform, PositionOf(this.comp.patrol.targetWaypoint), this.comp.status.patrolRotationSpeed, this.comp.status.patrolSpeed);
    }
    public void Search(){
        transform.Rotate(new Vector3(0, 150*Time.deltaTime, 0));
    }
 
    public void Attack(){
        Shoot(4);
        movement.RotateTo(this.transform, PositionOf(this.comp.fieldOfView.ClosestTarget), this.comp.status.chaseRotationSpeed*5); //faces target
    }
    public void Chase(){
        Shoot(2);
        movement.GoTo(this.transform, PositionOf(this.comp.fieldOfView.ClosestTarget), this.comp.status.chaseRotationSpeed*5, this.comp.status.chaseSpeed);
    }

    /// --------------------------------------------------------------------------------------------------------//////
    /// ------------  ALL METHODS UNDER THIS LINE ARE USED INSIDE THE METHODS ABOVE --------------------------- //////
    /// --------------------------------------------------------------------------------------------------------//////

    private Vector3 PositionOf(Transform t){
        return t.position;
    }
   public void Shoot(float _rate){
        charge += Time.deltaTime * comp.status.fireRate * _rate;
        if (charge >=1){
            comp.bulletsPool.Fire();
            charge = 0;
        }
    }
    private void AStartTo(Vector3 finalPos){
        CreatePathTo(finalPos);
        movement.GoTo(this.transform, this.comp.aStar.currentTargetWaypoint, this.comp.status.aStarRotationSpeed, this.comp.status.aStarSpeed);
    }
    private void CreatePathTo(Vector3 finalPos){
        if(this.comp.aStar.canRequestAPath){
            this.comp.aStar.RequestAPath(finalPos);
        }
        this.comp.aStar.UpdatePathfindingWay();
    }
}
