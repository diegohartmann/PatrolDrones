// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class IAAgentMovement
{
    public void GoTo(Transform _agentTransform, Vector3 _target, float _rotSpeed, float _moveSpeed){
        RotateTo(_agentTransform, _target, _rotSpeed);
        MoveForward(_agentTransform, _moveSpeed);
    }
    public void RotateTo(Transform _agentTransform, Vector3 _target, float _rotSpeed) {
        var neededRotation = Quaternion.LookRotation(_target - _agentTransform.position);
        var interpolatedRotation = Quaternion.Slerp(_agentTransform.rotation, neededRotation, Time.deltaTime * _rotSpeed);
        _agentTransform.rotation = interpolatedRotation;
       
    }
    public void MoveForward(Transform _agentTransform, float _speed){
        _agentTransform.Translate(Vector3.forward * Time.deltaTime * (_speed));
    }
}
