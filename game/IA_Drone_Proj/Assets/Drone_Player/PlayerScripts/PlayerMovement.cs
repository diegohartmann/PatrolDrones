using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private bool rbMove = true;
    [SerializeField]private Transform rotatableObj = null;
    [SerializeField]private float rotationFactor = 3f;
    [SerializeField]private int groundLayer = 11;
    [SerializeField]private float moveSpeed = 6f;
    [SerializeField]private float rotateOffset = 0.3f;
    private Rigidbody thisRB;
    [HideInInspector]public bool isOnTile;
    private Camera mainCamera;
    private void Awake() {
        SetStuff();
    }
    void SetStuff(){
        mainCamera = FindObjectOfType<Camera>();
        thisRB = GetComponent<Rigidbody>();
    }

    public void Movement(){
        Move();
        TurnToMouse();
        CheckIfIsOnTile();
    }
  
    void CheckIfIsOnTile(){
        RaycastHit hit;
        bool rayCollided = Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
        if(rayCollided){
            this.isOnTile = ( hit.collider.gameObject.layer == (groundLayer) );
            return;
        }
        this.isOnTile = false;
    }
    void TurnToMouse(){
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;
        if(groundPlane.Raycast(cameraRay, out rayLenght)){
            Vector3 pointToLook = (cameraRay.GetPoint(rayLenght));
            Vector3 finalPoint = new Vector3 (pointToLook.x,  rotateOffset  , pointToLook.z);
            RotateTo(finalPoint);
        }
    }
    void Move(){
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).normalized;
        if(rbMove){
            MoveRB(dir);
            return;
        }
        MoveTransform(dir);
    }
    void MoveTransform(Vector3 dir){
        transform.Translate(dir * DeltaTime(moveSpeed));
    }
    void MoveRB(Vector3 dir){
        dir *= DeltaTime(moveSpeed);
        thisRB.velocity = Vector3.zero;
        thisRB.angularVelocity = Vector3.zero;   
        thisRB.Sleep();
        thisRB.MovePosition(transform.position + dir);
    }
    void RotateTo(Vector3 _target){
        var neededRotation = Quaternion.LookRotation(_target - rotatableObj.position);
        var interpolatedRotation = Quaternion.Slerp(rotatableObj.rotation, neededRotation, Time.deltaTime * rotationFactor);
        if(rbMove){
            thisRB.MoveRotation(interpolatedRotation);
            return;
        }
        rotatableObj.rotation = interpolatedRotation;
    }
    float DeltaTime(float _times){
        return _times * Time.deltaTime;
    }
}
