using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionComponents : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public IAAgentPathFinding aStar;
    [HideInInspector] public MinionStatus status;
    [HideInInspector] public MinionStateChecker stateChecker;
    [HideInInspector] public MinionActions actions;
    [HideInInspector] public DamageFromBullet damageFromBullet;
    [HideInInspector] public MinionTriggerChecker triggerChecker;

    public void MinionComponentsInit() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //===============================================================
        status = GetComponent<MinionStatus>();
        actions = GetComponent<MinionActions>();
        stateChecker = GetComponent<MinionStateChecker>();
        damageFromBullet = GetComponent<DamageFromBullet>();
        triggerChecker = GetComponent<MinionTriggerChecker>();
        aStar = new IAAgentPathFinding(this.transform);
    }
}
