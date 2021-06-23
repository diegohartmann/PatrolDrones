using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawAgentInit : MonoBehaviour
{
    private void Awake(){
        GetComponent<DamageFromBullet>().DamageFromBulletInit();
    }
}
