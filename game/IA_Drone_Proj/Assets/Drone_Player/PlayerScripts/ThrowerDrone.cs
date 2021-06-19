using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerDrone : MonoBehaviour
{
    [HideInInspector] public PlayerWaspAttack waspAttack;
    [HideInInspector] public Vector3 finalPos;
    [SerializeField] private float moveSpeed = 6;
    // [SerializeField] private float speedMult = 1;
    bool rotate = true;
    private void OnEnable(){
        rotate = true;
    }
    private void Update(){
        if(rotate){
            RotateTo(finalPos, true, (moveSpeed+0.5f));
        }
        MoveForward(moveSpeed);
        if(Vector3.Distance(transform.position, finalPos) < 0.5f){
            waspAttack.ThrowBomb();
            rotate = false;
            StartCoroutine(DesableDrone(3));
        }
    }
    private IEnumerator DesableDrone( float t ){
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
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
}
