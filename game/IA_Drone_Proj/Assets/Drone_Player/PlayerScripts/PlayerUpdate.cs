﻿using System.Collections;
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
        GettingComponents();
        pool.SetUpBullets(fire);
    }
    private void Update() {
        if(Time.timeScale <= 0){
            return;
        }
        PlayerUpdateMethods();
        cameraController.CheckSwapCameraMode();
    }
    private void LateUpdate(){
        cameraController.CameraMovement();
    }
    private void PlayerUpdateMethods(){
        move.Movement();
        fire.CheckFire();
        wasps.CheckWapsAttack();
    }

    private void GettingComponents(){
        fire = GetComponent<PlayerFire>();
        move = GetComponent<PlayerMovement>();
        wasps = GetComponent<PlayerWaspAttack>();
        pool = GetComponent<PlayerBulletsPool>();
    }
}
