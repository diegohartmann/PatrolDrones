using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class was developed without tuto

public class DroneStatus : MonoBehaviour
{
    [Header ("SEARCH")]
    public const int maxSearchTimerValue = 6;
    public const int minSearchTimerValue = 0;
    [HideInInspector]public float searchTimer = 0;
    
    [Header ("CHASE")]
    [Range(1, 10)]public float chaseSpeed = 2;
    [Range(1, 10)] public float chaseRotationSpeed = 2;
    
    [Header ("BACK TO PATROL")]
    [Range(1, 10)]public float aStarSpeed = 2f;
    [Range(1, 10)]public float aStarRotationSpeed = 3f;

    [Header ("ATTACK")]
    [Range(0, 4)]public float distanceToAttack = 3f;
    // [Range(10, 20)]public float bulletSpeed = 15f;

    // [Range(0.01f, 1.0f)]public float shootDamage = 0.01f;
    // [Range(0.1f, 10.0f)]public float fireRate = 5f;
    
    [Header ("PATROL")]
    [Range(1, 10)]public float patrolSpeed = 2.5f;
    [Range(1, 10)] public float patrolRotationSpeed = 2;
}
