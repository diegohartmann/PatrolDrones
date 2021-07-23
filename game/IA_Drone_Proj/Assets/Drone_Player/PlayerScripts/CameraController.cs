using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode{
    ZoomOut,
    FollowPlayer,
}
public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraMode cameraMode = CameraMode.FollowPlayer;
    [Header("zoomOut")]
    [SerializeField] private KeyCode zoomButtom = KeyCode.Space; 
    private Camera thisCam;
    [SerializeField]private bool followPlayer = false;
    private const float zoomSmoothSpeed = 0.8f; //the higher the faster
    [SerializeField]private  Transform zoomCamTarget = null;
    private Vector3 zoomOffset = new Vector3 (0,0,0);
    [Header("Seguir player")]
    [SerializeField] private Transform followTarget = null;
    private const float playerSmoothSpeed = 0.5f; //the higher the faster
    private Vector3 playerOffset = new Vector3 (0,8,0);
    private void Awake() {
        thisCam = GetComponent<Camera>();
        ApplyMode(followPlayer);
    }
    public void CheckSwapCameraMode(){
        if(Input.GetKeyDown(zoomButtom)){
            ApplyMode(ModeToggle());
        }
    }
    public void CameraMovement(){
        switch (cameraMode){
            case CameraMode.ZoomOut:
                ZoomOut();
            break;
            case CameraMode.FollowPlayer:
                FollowPlayer();
            break;
        }
    }
    private bool ModeToggle(){
        followPlayer = !followPlayer;
        return followPlayer;
    }
    private void ApplyMode(bool _followPlayer){
        cameraMode = _followPlayer? CameraMode.FollowPlayer : CameraMode.ZoomOut;
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
