using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class bulletMovement : MonoBehaviour
{
    private GameObject shooter;
    private float fireSpeed;
    private float fireDamage;
  
    public void SingleBulletInit(GameObject _shooter, ShooterData _data){
        this.fireSpeed = _data.fireSpeed;
        this.fireDamage = _data.fireDamage;
        this.shooter = _shooter;
    }
    private void OnEnable(){
        StartCoroutine(DestroyBullet(2));
    }
    private IEnumerator DestroyBullet(float _time){
        yield return new WaitForSeconds(_time);
        gameObject.SetActive(false);
    }
    private void Update(){
        transform.Translate(Vector3.forward * Time.deltaTime * this.fireSpeed);
    }
    public float Damage(){
        return this.fireDamage;
    }
    public GameObject Shooter(){
        return shooter;
    }
}
