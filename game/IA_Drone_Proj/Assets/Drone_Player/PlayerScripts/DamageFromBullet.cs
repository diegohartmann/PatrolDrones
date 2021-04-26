using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFromBullet : MonoBehaviour
{
    [SerializeField] private bool isPlayerOrMinion = false;
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Transform GFXChild = null;
    private float currHealth = 0;
    private SceneLoader loader;
    private void Awake() {
        loader = FindObjectOfType<SceneLoader>();
        currHealth = maxHealth;
    }
    private void OnTriggerEnter(Collider other) {
        
        GameObject otherObj = other.gameObject;
        if (otherObj.CompareTag("Bullet"))
        {
            float damage = -otherObj.GetComponent<bulletMovement>().bullDamage;
            UpdateLife(damage);
            otherObj.SetActive(false);
        }
    }

    void UpdateLife(float _amt){
        currHealth += _amt;
        if(currHealth <= 0){
            currHealth = 0;
            SliderValue(0);
            DieCondition();
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
            return;
        }
        print("Vida do "+this.gameObject.name+" = "+currHealth+" (nao tem slider ainda)");
    }

    private void DieCondition(){
        if(isPlayerOrMinion){
            loader.Load("IA_Eexemple", 1);
        }
        DestroyDrone();
    }

    private void DestroyDrone(){
        foreach (Transform part in GFXChild)
        {
            var partObj = part.gameObject;
            part.parent = null;
            partObj.AddComponent<Rigidbody>();
            var col = partObj.AddComponent<MeshCollider>();
            col.convex = true;
            Destroy(this.gameObject);
            Destroy(partObj, 3);
        }
    }
}
