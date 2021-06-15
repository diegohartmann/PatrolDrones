using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    private PlayerFire fire;
    private exempleMove move;
    private PlayerWaspAttack wasps;
    private PlayerBulletsPool pool;
    [SerializeField] private CameraController cameraController = null;
    
    private void Awake() {
        GettingComponents();
        pool.SetUpBullets(fire);
    }
    private void Update() {
        if(Time.timeScale <= 0){
            return;
        }
        PlayerUpdateMethods();
        cameraController.SwapCameraMode();
    }
    private void LateUpdate(){
        cameraController.CheckCameraMode();
    }
    private void PlayerUpdateMethods(){
        move.Movement();
        fire.CheckFire();
        wasps.CheckWapsAttack();
    }

    private void GettingComponents(){
        fire = GetComponent<PlayerFire>();
        move = GetComponent<exempleMove>();
        wasps = GetComponent<PlayerWaspAttack>();
        pool = GetComponent<PlayerBulletsPool>();
    }
}
