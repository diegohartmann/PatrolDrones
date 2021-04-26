using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : MonoBehaviour
{

    private MinionComponents components;

    private void Awake(){
        components = GetComponent<MinionComponents>();
    }
    private Transform Player(){
        return components.player;
    }
    public void SimpleFollowPlayer(){
        if(Player()!=null){
            RotateTo(Player().position, true, components.status.rotationSpeed);
            MoveForward(components.status.runSpeed);
        }
    }

    public void AStartToPlayer(){
        if(Player()!=null){
            AStartTo(Player().position);
        }
    }

    public void GoToArea(Vector3 _position){
        AStartTo(_position);
    }
    
    /// --------------------------------------------------------------------------------------------------------//////
    /// ------------  ALL METHODS UNDER THIS LINE ARE USED INSIDE THE METHODS ABOVE --------------------------- //////
    /// --------------------------------------------------------------------------------------------------------//////


    private void RotateTo(Vector3 _target, bool _isSmooth, float _rotSpeed = 1) {
        if (_isSmooth){
            var neededRotation = Quaternion.LookRotation(_target - transform.position);
            var interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * _rotSpeed);
            transform.rotation = interpolatedRotation;
            return;
        }
        // else{
            transform.LookAt(_target);
        // }
        
    }

    private void MoveForward(float _speed){
        transform.Translate(Vector3.forward * Time.deltaTime * (_speed));
    }
    
    private void AStartTo(Vector3 finalPos){
        components.aStar.RequestAPath(finalPos);
        components.aStar.UpdatePathWaypoints();
        RotateTo(components.aStar.currentTargetWaypoint, true, components.status.rotationSpeed);
        MoveForward(components.status.runSpeed);    
    }
}
