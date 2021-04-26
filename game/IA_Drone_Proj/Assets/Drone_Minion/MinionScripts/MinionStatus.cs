using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStatus : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed;
    public float rotationSpeed;
    public bool isLocked = true;
    public bool isGoingToFixedArea = false;
    public bool hasToWait = false;
}
