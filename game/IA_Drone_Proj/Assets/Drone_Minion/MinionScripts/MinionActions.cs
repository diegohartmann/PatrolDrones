using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : MonoBehaviour
{
    private IAAgentMovement movement;
    private MinionComponents components;
    public void ActionsInit(MinionComponents _comp){
        components = _comp;
        movement = new IAAgentMovement();
    }
    public void StandOnPlayer(){

    }
    public void Locked(){

    }
    public void Stoped(){

    }
    public void SimpleFollowPlayer(){
        movement.GoTo(this.transform, PlayerPos(), components.status.rotationSpeed, components.status.runSpeed);
    }
    public void AStartToPlayer(){
        AStartTo(PlayerPos());
    }
    private Vector3 PlayerPos(){
        return components.player.position;
    }
    public void GoToArea(Vector3 _position){
        AStartTo(PlayerPos());
    }
    private void AStartTo(Vector3 finalPos){
        var _aStart = components.aStar;
        _aStart.RequestAPath(finalPos);
        _aStart.UpdatePathfindingWay();
        Vector3 _targetPos = _aStart.currentTargetWaypoint;
        if( _targetPos != null){
            movement.GoTo(this.transform, _targetPos, components.status.rotationSpeed, components.status.runSpeed);
        }
    }
}
