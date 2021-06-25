using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspUpdate : MonoBehaviour
{
    private FlockAgent flockAgent;
    private void Awake(){
        GetComponent<DamageFromBullet>().DamageFromBulletInit();
        flockAgent = GetComponent<FlockAgent>();
        flockAgent.Init( FindObjectOfType<Flock>(), GetComponent<Collider>() );
    }
    private void Update() {
        flockAgent.MoveFlockAgent();
    }
}
