using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStatus : MonoBehaviour
{
    [Header("Movement")]
    [Range(1,5)]public float runSpeed = 3;
    [Range(1,5)]public float rotationSpeed = 3;
}
