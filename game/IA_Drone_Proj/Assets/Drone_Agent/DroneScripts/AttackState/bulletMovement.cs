using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class bulletMovement : MonoBehaviour
{
    [HideInInspector]public float bullSpeed;
    [HideInInspector]public float bullDamage;
    [HideInInspector]public GameObject shooter;
  
    private void OnEnable(){
        StartCoroutine(DestroyBullet(2));
    }
    private void Update(){
        transform.Translate(Vector3.forward * Time.deltaTime * bullSpeed);
    }
    private IEnumerator DestroyBullet(float _time){
        yield return new WaitForSeconds(_time);
        gameObject.SetActive(false);
    }
}
