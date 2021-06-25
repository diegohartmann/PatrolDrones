using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private BulletsPool pool;
    private ShooterData data;
    
    private float charge = 0;
    
    public void FireInit(BulletsPool _pool, ShooterData _data) {
        pool = _pool;
        data = _data;
    }    
    public void ShootIf(bool _fire, float _rate = 1){
        if(_fire){
            Shoot(_rate);
        }
    }
    private void Shoot(float _rate){
        charge += Time.deltaTime * _rate * data.fireRate;
        if (charge >=1){
            _Fire();
            charge = 0;
        }
    }
    private void _Fire(){
        foreach (Transform _firePoint in pool.GetFirePointsHandler()){
            List<GameObject> BulletsReadyToBeShooted = new List<GameObject>();
            foreach (var bullet in pool.GetInstantiatedBullets()){
                if (!bullet.activeInHierarchy){
                    if(BulletsReadyToBeShooted.Count == 0){
                        BulletsReadyToBeShooted.Add(bullet);
                    }
                }
            }
            GameObject _bulletToShoot = BulletsReadyToBeShooted[0];
            _bulletToShoot.transform.position = _firePoint.position;
            _bulletToShoot.transform.rotation = _firePoint.rotation;
            _bulletToShoot.SetActive(true);
        }
    }
}
