using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : MonoBehaviour
{
    private MinionComponents components;
    public void ActionsInit(MinionComponents _comp){
        components = _comp;
    }
    private Vector3 PlayerPos(){
        return components.player.position;
    }
    public void SimpleFollowPlayer(){
        p("simple following Player");
        RotateTo(PlayerPos(), true, components.status.rotationSpeed);
        MoveForward(components.status.runSpeed);
    }
    public void AStartToPlayer(){
        p("aStar to Player");
        AStartTo(PlayerPos());
    }
    public void GoToArea(Vector3 _position){
        AStartTo(PlayerPos());
    }
    // public void Flocking(){
    //     // AStartTo(MinionsNetworking.leaderMinion.transform.position);
    //     p("flocking");
    //     components.flockAgent.MoveFlockAgent();
    //     // RotateTo(MinionsNetworking.leaderMinion.transform.position, true);
    // }
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
        MinionPathfinding _aStart = components.aStar;
        _aStart.RequestAPath(finalPos);
        _aStart.UpdatePathWaypoints();
        Vector3 _targetPos = _aStart.currentTargetWaypoint;
        if( _targetPos != null){
            RotateTo(_targetPos, true, components.status.rotationSpeed);
            MoveForward(components.status.runSpeed);
        }
    }
    void p (string _string){
        //print(_string);
    }
}
