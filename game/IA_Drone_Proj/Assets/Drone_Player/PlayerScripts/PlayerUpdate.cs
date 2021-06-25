using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    private Fire fire;
    private PlayerMovement move;
    private ShooterData shooterData;
    private PlayerWaspAttack wasps;
    private BulletsPool pool;
    [SerializeField] private CameraController cameraController = null;
    private void Awake() {

        move = GetComponent<PlayerMovement>();
            move.MovementInit();

        wasps = GetComponent<PlayerWaspAttack>();
            wasps.WaspAttackInit();

        shooterData = GetComponent<ShooterData>();

        pool = GetComponent<BulletsPool>();
            pool.BulletsPoolInit(shooterData);
        fire = new Fire(pool, shooterData);

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
        fire.ShootIf(Input.GetMouseButton(0));
        wasps.CheckWapsAttack();
        cameraController.CheckSwapCameraMode();
    }
    private void LateUpdate(){
        cameraController.CameraMovement();
    }
}
