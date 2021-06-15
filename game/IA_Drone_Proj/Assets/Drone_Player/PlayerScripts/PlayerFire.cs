using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]PlayerBulletsPool pool = null;
    public float fireRate = 10f;
    public float bulletSpeed = 20f;
    public float bulletDamage = 0.01f;
    public float charge = 0;    

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
