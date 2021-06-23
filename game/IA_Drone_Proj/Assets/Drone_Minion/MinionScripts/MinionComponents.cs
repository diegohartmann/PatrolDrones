using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionComponents : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public MinionPathfinding aStar;
    [HideInInspector] public MinionStatus status;
    [HideInInspector] public MinionStateChecker stateChecker;
    [HideInInspector] public MinionActions actions;
    [HideInInspector] public DamageFromBullet damageFromBullet;
    [HideInInspector] public MinionTriggerChecker triggerChecker;
    // [HideInInspector] public PathRequestManager pathRequest;


    public void MinionComponentsInit() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //===============================================================
        aStar = GetComponent<MinionPathfinding>();
        status = GetComponent<MinionStatus>();
        actions = GetComponent<MinionActions>();
        stateChecker = GetComponent<MinionStateChecker>();
        damageFromBullet = GetComponent<DamageFromBullet>();
        triggerChecker = GetComponent<MinionTriggerChecker>();
        // pathRequest = GetComponent<PathRequestManager>();
    }
}
