using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWaspAttack : MonoBehaviour{
    [SerializeField] private GameObject waspsHolder = null;
    [SerializeField] private float attackTime = 1;
    [Header("Fuel")]
    [SerializeField] private float startWaspFuel = 0;
    [SerializeField] private float maxWaspFuel = 5;
    private bool canUseWasps = true;
    private float waspFuel;
    private Camera mainCamera;
    private void Awake() {
        mainCamera = FindObjectOfType<Camera>();
        waspFuel = startWaspFuel;
    }
    public void CheckWapsAttack(){
        if(Input.GetMouseButtonDown(1)){
            SetAttackPosition();
            CheckAttack();
            return;
        }
    }
    void CheckAttack(){
        if(canUseWasps && (waspFuel > 0)){
            Attack();
        }
    }
    void Attack(){
        UpdateWaspFuel(-(attackTime/Time.deltaTime));
        SetWasp(true);            
    }
    
    public void UpdateWaspFuel(float _amt){
        waspFuel +=_amt;
        if(waspFuel > maxWaspFuel){
            waspFuel = maxWaspFuel;
            return;
        }
        if(waspFuel < 0){
            waspFuel = 0;
            return;
        }
    }
    private void SetWasp(bool b){
        waspsHolder.SetActive(b);
    }

    private void SetAttackPosition(){
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;
        if(groundPlane.Raycast(cameraRay, out rayLenght)){
            Vector3 pointToLook = (cameraRay.GetPoint(rayLenght));
            Vector3 finalPoint = new Vector3 (pointToLook.x,  0.3f  , pointToLook.z);
            SetWaspsPosition(finalPoint);
            return;
        }
        Debug.LogWarning("impossivel atacar aqui. nao há chao");
    }
    private void SetWaspsPosition(Vector3 pos){
        waspsHolder.transform.position = new Vector3 (pos.x, 0.3f, pos.z);
    }
}
