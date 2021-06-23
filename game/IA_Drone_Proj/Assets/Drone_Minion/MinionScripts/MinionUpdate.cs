using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionUpdate : MonoBehaviour
{
    private MinionComponents comp;
    private void Awake() {
        MinionInit();
    }
    private void Update(){
        _MinionUpdate();
    }
    private void MinionInit(){
        comp = GetComponent<MinionComponents>();
            comp.MinionComponentsInit();

        comp.stateChecker.StateCheckerInit(comp);
        comp.actions.ActionsInit(comp);
        comp.triggerChecker.TriggerCheckerInit();
        comp.damageFromBullet.DamageFromBulletInit();
    }
    private void _MinionUpdate(){
        comp.stateChecker.MinionStateMachine();
    }
}
