using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DamageFromBullet : MonoBehaviour
{
    [SerializeField] private bool isPlayerOrMinion = false;
    [SerializeField] private bool destructble = true;
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Transform GFXChild = null;
    [SerializeField] private UnityEvent OnDestroyed = null;
    private float currHealth = 0;
    private SceneLoader loader;
    
    private void Awake() {
        loader = FindObjectOfType<SceneLoader>();
        currHealth = maxHealth;
    }
    private void OnTriggerEnter(Collider other) {
        GameObject otherObj = other.gameObject;
        if (otherObj.CompareTag("Bullet")){
            bulletMovement bulletMov = otherObj.GetComponent<bulletMovement>();
            if(SameObj(bulletMov.shooter, gameObject)){
                return;
            }
            if(destructble){
                UpdateLife(-bulletMov.bullDamage);
            }
            otherObj.SetActive(false);
        }
    }
    void UpdateLife(float _amt){
        currHealth += _amt;
        if(currHealth <= 0){
            currHealth = 0;
            SliderValue(0);
            DieCondition();
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
    private void DieCondition(){
        if(isPlayerOrMinion){
            loader.Load("IA_Eexemple", 1);
        }
        DestroyDrone();
    }
    private void DestroyDrone(){
        if(GFXChild!= null){
            foreach (Transform part in GFXChild)
            {
                GameObject partObj = part.gameObject;
                part.parent = null;
                partObj.AddComponent<Rigidbody>();
                if(partObj.GetComponent<Collider>() == null){
                    var col = partObj.AddComponent<MeshCollider>();
                    col.convex = true;
                }
                Destroy(partObj, 3);
            }
        }
        Destroy(this.gameObject);
    }
    private bool SameObj(GameObject obj1, GameObject obj2){
        return obj1 == obj2;
    }

    private void OnDestroyEvent(){
        if(OnDestroyed!=null){
                OnDestroyed.Invoke();
        }
    }
}
