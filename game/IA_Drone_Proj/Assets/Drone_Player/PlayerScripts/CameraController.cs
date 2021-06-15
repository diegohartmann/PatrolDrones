using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode{
    FollowingTarget,
    ZoomedOut,
}
public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraMode cameraMode;
    [Header("zoomOut")]
    [SerializeField] private KeyCode zoomButtom = KeyCode.Space; 
    private Camera thisCam;
    private float size;
    private bool zoomOut = false;
    [SerializeField][Range (0,1)] float zoomSmoothSpeed = 0.8f; //the higher the faster
    [SerializeField] Transform zoomCamTarget = null;
    // [SerializeField] float orthoSize = 10.5f;
    Vector3 zoomOffset = new Vector3 (0,0,0);
    [Header("Seguir player")]
    [SerializeField] Transform followTarget = null;
    [SerializeField][Range (0,1)] float playerSmoothSpeed = 0.5f; //the higher the faster
    [SerializeField] Vector3 playerOffset = new Vector3 (0,5,0);
    private void Awake() {
        thisCam = GetComponent<Camera>();
    }

    
    public void SwapCameraMode(){
        if(Input.GetKeyDown(zoomButtom)){
            
            zoomOut = !zoomOut;
            if(zoomOut){
                cameraMode = CameraMode.ZoomedOut;
                return;
            }            
            cameraMode = CameraMode.FollowingTarget;
        }
    }
    public void CheckCameraMode()
    {
        switch (cameraMode)
        {
            case CameraMode.FollowingTarget:
                ZoomOut();
            break;

            case CameraMode.ZoomedOut:
                FollowPlayer();
            break;

            default:
            break;
        }
    }
    private void ZoomOut(){
        Follow(zoomCamTarget, zoomOffset, zoomSmoothSpeed);
    }
    private void FollowPlayer(){
        if(followTarget!=null){
            Follow(followTarget, playerOffset, playerSmoothSpeed);
        }
    }

    private void Follow(Transform _target, Vector3 _offset, float smoothSpeed){
        Vector3 desiredPos = _target.position + _offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime*10* smoothSpeed);
        transform.position = smoothPos;
    }
}
