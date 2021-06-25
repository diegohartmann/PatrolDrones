using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class was developed without tuto
public class BulletsPool : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab = null;
    [SerializeField] private Transform FirePointsHandler = null;
    private List<GameObject> InstantiatedBullets = null;
    public void BulletsPoolInit(ShooterData _data){
        InstantiatedBullets = new List<GameObject>();
        foreach (Transform _firePoint in FirePointsHandler){
            for (int i = 0; i < (_data.fireRate * 5); i++){
                GameObject _bullet = Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
                _bullet.GetComponent<bulletMovement>().SingleBulletInit(this.gameObject, _data);
                _bullet.SetActive(false);
                InstantiatedBullets.Add(_bullet);
            }
        }
    }
    public Transform GetFirePointsHandler(){
        return FirePointsHandler;
    }
    public List<GameObject> GetInstantiatedBullets(){
        return InstantiatedBullets;
    }
}
