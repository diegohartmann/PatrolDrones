using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    private PlayerFire fire;
    private PlayerMovement move;
    private PlayerWaspAttack wasps;
    private PlayerBulletsPool pool;
    [SerializeField] private CameraController cameraController = null;
    private void Awake() {
        fire = GetComponent<PlayerFire>();

        move = GetComponent<PlayerMovement>();
            move.MovementInit();

        wasps = GetComponent<PlayerWaspAttack>();
            wasps.WaspAttackInit();

        pool = GetComponent<PlayerBulletsPool>();
            pool.SetUpBullets(fire);
            fire.FireInit(pool);

        DamageFromBullet damage = GetComponent<DamageFromBullet>(); 
        if(damage!= null){
            damage.DamageFromBulletInit();
        }    
    }
    private void Update() {
        if(Time.timeScale <= 0){
            return;
        }
        move.Movement();
        fire.CheckFire();
        wasps.CheckWapsAttack();
        cameraController.CheckSwapCameraMode();
    }
    private void LateUpdate(){
        cameraController.CameraMovement();
    }
}
