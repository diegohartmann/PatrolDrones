using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerDrone : MonoBehaviour
{
    [HideInInspector] public PlayerWaspAttack waspAttack;
    [HideInInspector] public Vector3 finalPos;
    [SerializeField] private float moveSpeed = 6;
    private IAAgentMovement movement;
    bool rotate = true;

    private void Awake(){
        movement = new IAAgentMovement();
    }
    private void OnEnable(){
        rotate = true;
    }
    private void Update(){
        if(rotate){
            movement.RotateTo(this.transform, finalPos, (moveSpeed+0.5f));
        }
        movement.MoveForward(this.transform, moveSpeed);
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
}
