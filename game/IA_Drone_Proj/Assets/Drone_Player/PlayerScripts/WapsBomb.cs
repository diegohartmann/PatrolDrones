using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WapsBomb : MonoBehaviour
{
    [SerializeField]private int groundLayer=11;
    [HideInInspector]public PlayerWaspAttack waspAttack;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == groundLayer){
            waspAttack.BlowBomb();
        }
    }
}