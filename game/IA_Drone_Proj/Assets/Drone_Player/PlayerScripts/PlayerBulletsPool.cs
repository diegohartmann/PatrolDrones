using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class PlayerBulletsPool : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] Transform FirePointsHandler = null;
    private List<GameObject> InstantiatedBullets = null;
    
    public void SetUpBullets(PlayerFire _fire){
        InstantiateBullets(_fire);
    }
    private void InstantiateBullets(PlayerFire fire){
        InstantiatedBullets = new List<GameObject>();
        foreach (Transform _firePoint in FirePointsHandler){
            for (int i = 0; i < (fire.fireRate * 10); i++){
                GameObject _bullet = Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
                InstantiatedBullets.Add(_bullet);
                bulletMovement bull = _bullet.GetComponent<bulletMovement>();
                bull.shooter = this.gameObject;
                _bullet.SetActive(false);
            }
        }
    }
    public void Fire(PlayerFire fire){
        foreach (Transform _firePoint in FirePointsHandler){
            
            List<GameObject> BulletsReadyToBeShooted = new List<GameObject>();;
            
            foreach (var bullet in InstantiatedBullets){
                if (!bullet.activeInHierarchy){
                    if(BulletsReadyToBeShooted.Count == 0){
                        BulletsReadyToBeShooted.Add(bullet);
                    }
                }
            }

            GameObject _bulletToShoot = BulletsReadyToBeShooted[0];
            _bulletToShoot.transform.position = _firePoint.position;
            _bulletToShoot.transform.rotation = _firePoint.rotation;
            bulletMovement bull = _bulletToShoot.GetComponent<bulletMovement>();
            bull.bullSpeed = fire.bulletSpeed;
            bull.bullDamage = fire.bulletDamage;
            
            _bulletToShoot.SetActive(true);
        }
    }
    private bulletMovement BulletMovFrom(GameObject obj){
        return obj.GetComponent<bulletMovement>();        
    }
}
