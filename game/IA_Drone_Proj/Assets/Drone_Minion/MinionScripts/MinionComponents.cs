﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionComponents : MonoBehaviour
{
    [HideInInspector] public MinionPathfinding aStar;
    [HideInInspector] public MinionStatus status;
    [HideInInspector] public FlockAgent flockAgent;
    [HideInInspector] public MinionActions actions;
    [HideInInspector] public Transform player;

    private void Awake() {
        transform.parent = null;
        aStar = GetComponent<MinionPathfinding>();
        status = GetComponent<MinionStatus>();
        flockAgent = GetComponent<FlockAgent>();
        actions = GetComponent<MinionActions>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
