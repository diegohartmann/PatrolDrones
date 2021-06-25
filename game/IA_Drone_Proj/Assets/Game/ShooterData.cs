using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterData : MonoBehaviour
{
    [Range(5, 20)] public float fireRate = 10f;
    [Range(10, 30)]public float fireSpeed = 15f;
    [Range(0.01f, 1)]public float fireDamage = 0.01f;
}
