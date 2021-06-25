using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DroneAgentUpdate : MonoBehaviour
{
    private DroneComponents comp;
    //==========================================
    private void Awake(){
        DronesNetworkComunication.dronesViewingIntruser.Clear();
        comp = GetComponent<DroneComponents>();
        comp.ComponentsInit();
        comp.status.searchTimer = DroneStatus.minSearchTimerValue;
        comp.bulletsPool.BulletsPoolInit(comp);
        comp.fieldOfView.FieldOfViewInit();
        comp.actions.ActionsInit(comp);
        comp.patrol.PatrolPointsInit();
        comp.damageFromBullets.DamageFromBulletInit();
    }
    private void Update(){
        comp.fieldOfView.FindVisibleTargets();
        comp.stateChecker.SetState(comp);
        comp.stateChecker.ExecuteState(comp.actions);
    }
    private void LateUpdate() {
        comp.fieldOfView.DrawFieldOfView();
    }
}
