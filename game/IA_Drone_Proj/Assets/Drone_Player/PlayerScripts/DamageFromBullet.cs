using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DamageFromBullet : MonoBehaviour
{
    [SerializeField] private bool destructble = true;
    [SerializeField] [Range(0.1f, 1.0f)] private float maxHealth = 1;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private UnityEvent OnDestroyed = null;
    private PlayerWaspAttack player;
   
    private float currHealth = 0;
    private SceneLoader loader;
    private void Awake() {
        player = FindObjectOfType<PlayerWaspAttack>();
        loader = FindObjectOfType<SceneLoader>();
        currHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other) {
        CheckTrigger(other.gameObject);
    }
    private void CheckTrigger(GameObject otherObj){
        if (otherObj.CompareTag("Bullet")){
            bulletMovement bulletMov = otherObj.GetComponent<bulletMovement>();
            if(!SameObj(bulletMov.shooter, gameObject)){ //se o atirador nao acertou em si mesmo
                DestroyBulletEffect(otherObj);
                if(destructble){
                    UpdateLife(-bulletMov.bullDamage);
                }
            }
            return;
        }
        if (otherObj.CompareTag("Wasp")){
            if(destructble){
                UpdateLife(-0.01f);
            }
        }
    }

    private void DestroyBulletEffect(GameObject _bullet){
        _bullet.SetActive(false);
        //som
        //efeito visual
    }


    void UpdateLife(float _amt){
        currHealth += _amt;
        if(currHealth <= 0){
            currHealth = 0;
            SliderValue(0);
            OnDestroyEvent();
            return;
        }
        if(currHealth > maxHealth){
            currHealth = maxHealth;
            SliderValue(maxHealth);
            return;
        }
        SliderValue(currHealth);
    }
    void SliderValue(float _value){
        if(healthSlider != null){
            healthSlider.value = _value;
        }
    }
    private bool SameObj(GameObject obj1, GameObject obj2){
        return obj1 == obj2;
    }
    private void OnDestroyEvent(){
        if(OnDestroyed!=null){
            OnDestroyed.Invoke();
        }
    }
    public void SplashObjectsIn(Transform partsParent){
        foreach (Transform part in partsParent)
        {
            Splash(part.gameObject);
        }
    }
    public void SplashObject(GameObject _object){
        Splash(_object);
    }
    private void Splash(GameObject _object){
        if(_object.GetComponent<Collider>() == null){
            var col = _object.AddComponent<MeshCollider>();
            col.convex = true;
        }
        if(_object.GetComponent<Rigidbody>() == null){
            _object.AddComponent<Rigidbody>();
        }
        
        _object.transform.parent = null;
        Destroy(_object, 3f);
    }
    public void _DestroyObject(GameObject _object){
        Destroy(_object);
    }
    public void _DestroyObjectsIn(Transform partsParent){
        foreach (Transform part in partsParent)
        {
            Destroy(part.gameObject);
        }
    }
    public void ReloadGame(float t){
        loader.Reload(t);
    }
    public void IncrementDeadDrones(int _amt){
        if(DronesNetworkComunication.deadDrones < 3 ){
            player.IncrementDeadDrones(_amt);
            player.FillFuel();
        }
    }
}
