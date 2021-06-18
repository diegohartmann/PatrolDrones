using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]PlayerBulletsPool pool = null;
    [Range(1,20)]public float fireRate = 10f;
    [Range(10,30)]public float bulletSpeed = 20f;
    [Range(0.005f, 0.3f)]public float bulletDamage = 0.01f;
    private float charge = 0;    

    public void CheckFire(){
        if(Input.GetMouseButton(0)){
            Shoot(1);
        }
    }
    private void Shoot (float _rate){
        charge += Time.deltaTime * fireRate * _rate;
        if (charge >=1){
            pool.Fire(this);
            charge = 0;
        }
    }
}
