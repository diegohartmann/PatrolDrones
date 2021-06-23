using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DroneAgentUpdate : MonoBehaviour
{
    private DroneComponents comp;
    //==========================================
    private void Awake(){
        DroneInit();
    }
    private void Update(){
        DroneUpdate();
    }
    private void LateUpdate() {
        DroneLateUpdate();
    }
    // ======== COLOCAR EM OUTRO ARQUIVO? =========
    private void DroneInit(){
        DronesNetworkComunication.dronesViewingIntruser.Clear();
        comp = GetComponent<DroneComponents>();
        comp.ComponentsInit();
        // comp.aStar.AStarInit(comp.pathRequest);
        comp.status.searchTimer = DroneStatus.minSearchTimerValue;
        comp.bulletsPool.BulletsPoolInit(comp);
        comp.fieldOfView.FieldOfViewInit();
        comp.actions.ActionsInit(comp);
        comp.patrol.PatrolPointsInit();
        comp.damageFromBullets.DamageFromBulletInit();
    }
    private void DroneUpdate(){
        comp.fieldOfView.FindVisibleTargets();
        comp.stateChecker.SetState(comp);
        comp.stateChecker.ExecuteState(comp.actions);
    }
    private void DroneLateUpdate(){
        comp.fieldOfView.DrawFieldOfView();
    }
}
