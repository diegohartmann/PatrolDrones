using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class StateActions : MonoBehaviour
{
    // [SerializeField] Transform gfx = null;
    private DroneComponents comp;
    private float charge = 0;
    
    public void ActionsInit(DroneComponents _comp){
      comp = _comp;
    }
    /// --------------------------------------------------------------------------------------------------------//////
    /// -----  ALL METHODS UNDER THIS LINE ARE THE DRONE STATES, CALLED INTO StateChecker's Update() method ----//////
    /// --------------------------------------------------------------------------------------------------------//////
    // bool b = true;
    public void GoingBackToPatrol(){
        // comp.patrol.SmartWaypoints();
        comp.patrol.SimpleWaypoints();
        AStartTo(PositionOf(this.comp.patrol.targetWaypoint));
    }
    
    
    public void Patrol(){
        comp.patrol.SimpleWaypoints();
        RotateTo(PositionOf(this.comp.patrol.targetWaypoint), true, this.comp.status.patrolRotationSpeed);
        MoveForward(this.comp.status.patrolSpeed);
    }
    public void Search(){
        transform.Rotate(new Vector3(0, 150*Time.deltaTime, 0));
    }
 
    public void Attack(){
        Shoot(4);
        RotateTo(this.comp.fieldOfView.ClosestTarget.position, true, this.comp.status.chaseRotationSpeed*5); //faces target
    }
    public void Chase(){
        Shoot(2);
        RotateTo(this.comp.fieldOfView.ClosestTarget.position, true, this.comp.status.chaseRotationSpeed); //faces target
        MoveForward(this.comp.status.chaseSpeed); //doesnt need A* cuz it follows the player in straight line, and once there is an obstacle between them, the drone loses sight of the player.                     
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

    private void RotateTo(Vector3 _target, bool _isSmooth, float _rotSpeed = 1) {
        if (_isSmooth){
            var neededRotation = Quaternion.LookRotation(_target - transform.position);
            var interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * _rotSpeed);
            this.transform.rotation = interpolatedRotation;
        }
        else{
            this.transform.LookAt(_target);
        }
    }
    private void MoveForward(float _speed){
        this.transform.Translate(Vector3.forward * Time.deltaTime * (_speed));
    }
    private void AStartTo(Vector3 finalPos){
        CreatePathTo(finalPos);
        RotateTo(this.comp.aStar.currentTargetWaypoint, true, this.comp.status.aStarRotationSpeed);
        MoveForward(this.comp.status.aStarSpeed);    
    }

    private void CreatePathTo(Vector3 finalPos){
        if(this.comp.aStar.canRequestAPath){
            this.comp.aStar.RequestAPath(finalPos);
        }
        this.comp.aStar.UpdatePathfindingWay();
    }
}
