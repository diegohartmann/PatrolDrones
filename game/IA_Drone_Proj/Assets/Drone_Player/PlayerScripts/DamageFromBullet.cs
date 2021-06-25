using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DamageFromBullet : MonoBehaviour
{
    [SerializeField] private bool destructble = true;
    private const int maxHealth = 1;
    [SerializeField] [Range(0.1f, 1.0f)] private float startHealth = 1;
    [SerializeField] private Transform healthImages = null;
    [SerializeField] private UnityEvent OnDestroyed = null;
    [SerializeField] private HealthColors healthColors = null;
    
    private PlayerWaspAttack player;
    private SceneLoader loader;
    private float currHealth;

    private const int bulletLayer = 13;
    private const int waspLayer = 14;
    private List <Image> HealthImages = new List<Image>();

    public void DamageFromBulletInit() {
        HealthImagesInit();
        VariablesInit();
        UpdateHealthFill(startHealth);
    }
    private void HealthImagesInit(){
        if(HasHealthImages()){
            foreach(Transform image in healthImages){
                HealthImages.Add(image.GetComponent<Image>());
            }
        }
    }
    private void VariablesInit(){
        player = FindObjectOfType<PlayerWaspAttack>();
        loader = FindObjectOfType<SceneLoader>();
        currHealth = startHealth;
    }
    private void OnTriggerEnter(Collider other) {
        GameObject otherObj = (other.gameObject);
        if (otherObj.layer == bulletLayer){
            bulletMovement bulletMov = otherObj.GetComponent<bulletMovement>();
            if(DifferentObjects(bulletMov.Shooter(), this.gameObject)){ 
                if(destructble){
                    UpdateLife(-bulletMov.Damage());
                }
                DestroyBulletEffect(otherObj);
            }
            return;
        }
        if (otherObj.layer == waspLayer){
            if(destructble){
                UpdateLife(-0.01f);
            }
        }
    }
    //COLOCAR RIGIDBODY NAS BULLETS E WASPS PRA DETECTAR COLISAO POR LAYER E NAO POR TAG
    private void DestroyBulletEffect(GameObject _bullet){
        _bullet.SetActive(false);
        //som
        //efeito visual
    }
    private void UpdateLife(float _amt){
        currHealth += _amt;
        if(currHealth <= 0){
            UpdateHealthFill(0);
            OnDestroyEvent();
            return;
        }
        if(currHealth > maxHealth){
            currHealth = maxHealth;
            UpdateHealthFill(maxHealth);
            return;
        }
        UpdateHealthFill(currHealth);
    }
    public void IsDestructable(bool b){
        destructble = b;
    }
    private void UpdateHealthFill(float _value){
        if(HasHealthImages()){
            if(HealthImages.Count > 0){
                foreach (Image item in HealthImages){
                    item.fillAmount = _value;
                    SetImageColor(item,
                        _value < 0.3f ? healthColors.lowLifeColor : 
                            _value < 0.6f? healthColors.midLifeColor :
                                _value < 1? healthColors.almostFullLifeColor: healthColors.fullLifeColor);
                }
            }
        }
    }
    private void SetImageColor(Image _image, Color _color){
        if(_image.color != _color){
            _image.color = _color;
        }
    }
    private bool HasHealthImages(){
        return healthImages != null;
    }
    private bool DifferentObjects(GameObject obj1, GameObject obj2){
        return obj1 != obj2;
    }
    private void OnDestroyEvent(){
            if(OnDestroyed!=null){
                OnDestroyed.Invoke();
            }
    }
    public void SplashObjectsIn(Transform partsParent){
        foreach (Transform part in partsParent){
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
        if(loader!=null){
            loader.Reload(t);
            return;
        }
        Debug.LogWarning("loader em "+name+" é nulo");
    }
    public void IncrementDeadDrones(int _amt){
        if(DronesNetworkComunication.deadDrones < 3 ){
            if(player == null){
                player = FindObjectOfType<PlayerWaspAttack>();
            }
            player.IncrementDeadDrones(_amt);
            player.FillFuel();
        }
    }
}
