using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class StateActions : MonoBehaviour
{
    private DroneComponents comp;
    private float charge = 0;
    

    private void Awake(){
      comp = GetComponent<DroneComponents>();
    }
    /// --------------------------------------------------------------------------------------------------------//////
    /// -----  ALL METHODS UNDER THIS LINE ARE THE DRONE STATES, CALLED INTO StateChecker's Update() method ----//////
    /// --------------------------------------------------------------------------------------------------------//////
    public void GoingBackToPatrol(){
        // if(comp.patrol.hasPatrolPoints()){
            AStartTo(comp.patrol.targetWaypoint.position);
            comp.patrol.SmartWaypoints();
            // return;
        // }
        // Search();
    }
    public void Patrol(){
        // if(comp.patrol.hasPatrolPoints()){
            comp.patrol.SimpleWaypoints();
            RotateTo(comp.patrol.targetWaypoint.position, true, comp.status.patrolRotationSpeed);
            MoveForward(comp.status.patrolSpeed);
            // return;  
        // }
        // Search();
    }
    public void Search(){
        transform.Rotate(new Vector3(0, 150*Time.deltaTime, 0));//AStartTo(randomTransforms);
    }
 
    public void Attack(){
        Shoot(4);
        RotateTo(comp.fieldOfView.ClosestTarget.position, true, comp.status.chaseRotationSpeed*5); //faces target
    }
    public void Chase(){
        Shoot(2);
        RotateTo(comp.fieldOfView.ClosestTarget.position, true, comp.status.chaseRotationSpeed); //faces target
        MoveForward(comp.status.chaseSpeed); //doesnt need A* cuz it follows the player in straight line, and once there is an obstacle between them, the drone loses sight of the player.                     
    }

    /// --------------------------------------------------------------------------------------------------------//////
    /// ------------  ALL METHODS UNDER THIS LINE ARE USED INSIDE THE METHODS ABOVE --------------------------- //////
    /// --------------------------------------------------------------------------------------------------------//////

   public void Shoot(float _rate){
        charge += Time.deltaTime * comp.status.fireRate * _rate;
        if (charge >=1){
            comp.bulletsPool.Fire();
            charge = 0;
        }
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
    private void MoveForward(float _speed){
        transform.Translate(Vector3.forward * Time.deltaTime * (_speed));
    }
    
    private void AStartTo(Vector3 finalPos){
        CreatePathTo(finalPos);
        RotateTo(comp.aStar.currentTargetWaypoint, true, comp.status.aStarRotationSpeed);
        MoveForward(comp.status.aStarSpeed);    
    }

    private void CreatePathTo(Vector3 finalPos){
        if(comp.aStar.canRequestAPath){
            comp.aStar.RequestAPath(finalPos);
        }
        comp.aStar.UpdatePathfindingWay();
    }
}
