using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]bool reset;
    [SerializeField] private Vector3 spinAxisSpeed = Vector3.zero;
    private void Update() {
        transform.Rotate(spinAxisSpeed*Time.deltaTime * 100);
        if (reset){
            spinAxisSpeed = Vector3.zero;
            transform.rotation = Quaternion.Euler(spinAxisSpeed);
            reset = false;
        }
    }
}
