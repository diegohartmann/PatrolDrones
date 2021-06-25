using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionUpdate : MonoBehaviour
{
    private MinionComponents comp;
    private void Awake() {
        comp = GetComponent<MinionComponents>();
        comp.MinionComponentsInit();
        comp.stateChecker.StateCheckerInit(comp);
        comp.actions.ActionsInit(comp);
        comp.triggerChecker.TriggerCheckerInit();
        comp.damageFromBullet.DamageFromBulletInit();
    }
    private void Update(){
        comp.stateChecker.MinionStateMachine();
    }
}
