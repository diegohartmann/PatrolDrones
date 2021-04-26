using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exempleMove : MonoBehaviour
{
    [SerializeField]private Transform GFX = null;
    [SerializeField]private float rotationFactor = 3f;
    [SerializeField]private float moveSpeed = 6f;
    [SerializeField]private float rotateOffset = 0.3f;
    private Camera mainCamera;
    private void Awake() {
        mainCamera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        TurnToMouse();
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).normalized;
        MoveTo(dir);
    }

    void TurnToMouse(){
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;
        if(groundPlane.Raycast(cameraRay, out rayLenght)){
            Vector3 pointToLook = (cameraRay.GetPoint(rayLenght));
            Vector3 finalPoint = new Vector3 (pointToLook.x,  rotateOffset  , pointToLook.z); 
            RotateTo(finalPoint); //followTransform follow mouse pos
        }
    }
    void MoveTo(Vector3 dir){
        transform.Translate(dir * Time.deltaTime * moveSpeed);
    }
    void RotateTo(Vector3 _target){
        var neededRotation = Quaternion.LookRotation(_target - GFX.position);
        var interpolatedRotation = Quaternion.Slerp(GFX.rotation, neededRotation, Time.deltaTime * rotationFactor);
        GFX.rotation = interpolatedRotation;
    }
}
