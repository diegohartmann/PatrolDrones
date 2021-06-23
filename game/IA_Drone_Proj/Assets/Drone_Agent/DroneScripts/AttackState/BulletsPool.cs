using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto

public class BulletsPool : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] Transform FirePointsHandler = null;
    private List<GameObject> InstantiatedBullets = null;
    private DroneComponents components;
    public void BulletsPoolInit(DroneComponents _components){
        components = _components;
        InstantiateBullets(_components.status);
    }
    public void InstantiateBullets(DroneStatus status){
        InstantiatedBullets = new List<GameObject>();
        foreach (Transform _firePoint in FirePointsHandler){
            for (int i = 0; i < (status.fireRate * 10); i++){
                GameObject _bullet = Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
                InstantiatedBullets.Add(_bullet);
                _bullet.SetActive(false);
            }
        }
    }
    public void Fire(){
        foreach (Transform _firePoint in FirePointsHandler){
            List<GameObject> BulletsReadyToBeShooted = new List<GameObject>();;
            
            foreach (var bullet in InstantiatedBullets)
                if (!bullet.activeInHierarchy)
                    if(BulletsReadyToBeShooted.Count == 0)
                        BulletsReadyToBeShooted.Add(bullet);

            GameObject _bulletToShoot = BulletsReadyToBeShooted[0];
            _bulletToShoot.transform.position = _firePoint.position;
            _bulletToShoot.transform.rotation = _firePoint.rotation;
            _bulletToShoot.SetActive(true);
            bulletMovement bullMov = _bulletToShoot.GetComponent<bulletMovement>();
            bullMov.bullSpeed = components.status.bulletSpeed;
           bullMov.bullDamage = components.status.shootDamage;
        }
    }
}
