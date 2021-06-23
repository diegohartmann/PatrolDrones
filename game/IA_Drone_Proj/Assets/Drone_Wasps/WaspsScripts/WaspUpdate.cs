using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspUpdate : MonoBehaviour
{
    private FlockAgent flockAgent;
    private void Awake(){
        WaspAwake();
    }
    private void Update() {
        _WaspUpdate();
    }
    private void WaspAwake(){
        flockAgent = GetComponent<FlockAgent>();
            flockAgent.FlockAgentInit();
        GetComponent<DamageFromBullet>().DamageFromBulletInit();
    }
    private void _WaspUpdate(){
        flockAgent.MoveFlockAgent();
    }
}
