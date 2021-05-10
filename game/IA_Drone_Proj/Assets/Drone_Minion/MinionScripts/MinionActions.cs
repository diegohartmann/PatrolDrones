using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : MonoBehaviour
{
    private MinionComponents components;
    private void Awake(){
        components = GetComponent<MinionComponents>();
    }
    private Vector3 PlayerPos(){
        return components.player.position;
    }
    public void SimpleFollowPlayer(){
        print("simple following Player");
        RotateTo(PlayerPos(), true, components.status.rotationSpeed);
        MoveForward(components.status.runSpeed);
    }
    public void AStartToPlayer(){
        print("aStar to Player");
        AStartTo(PlayerPos());
    }
    public void GoToArea(Vector3 _position){
        AStartTo(PlayerPos());
    }
    public void Flocking(){
        // AStartTo(MinionsNetworking.leaderMinion.transform.position);
        print("flocking");
        components.flockAgent.MoveFlockAgent();
        // RotateTo(MinionsNetworking.leaderMinion.transform.position, true);
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
        transform.LookAt(_target);
    }

    private void MoveForward(float _speed){
        transform.Translate(Vector3.forward * Time.deltaTime * (_speed));
    }
    
    private void AStartTo(Vector3 finalPos){
        var _aStart = components.aStar;
        _aStart.RequestAPath(finalPos);
        _aStart.UpdatePathWaypoints();
        RotateTo(_aStart.currentTargetWaypoint, true, components.status.rotationSpeed);
        MoveForward(components.status.runSpeed);
    }
}
