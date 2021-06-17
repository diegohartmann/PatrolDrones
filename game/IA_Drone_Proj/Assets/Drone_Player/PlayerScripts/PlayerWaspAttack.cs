using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaspAttack : MonoBehaviour{
    [SerializeField] private GameObject waspsHolder = null;
    [SerializeField] private GameObject wasps = null;
    [SerializeField] private Image waspsTimer = null;
    [SerializeField] private GameObject waspsAimEffect = null;
    [SerializeField] private KeyCode cancelAimButton = KeyCode.C;
    private bool canUseWasps = false;
    private Camera mainCamera;
    [SerializeField][Range(0,1)]private float fuel = 1;
    private bool waspsAreAttacking = false;
    [SerializeField] private float fuelTimer = 3;
    private void Awake() {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void CheckWapsAttack(){
        if(DronesNetworkComunication.deadDrones < 3){
            return;
        }
        if(fuel<= 0){
            fuel=0;
            return;
        }
        TougleAimCheck(MouseClick(1));
        if(canUseWasps){
            if(MouseHold(1)){
                SetAttackPosition();
                TougleAimCheck(CancelAimButton());
                return;
            }
            if(MouseRelease(1)){
                AimEffect(false);
                // if(fuel>=0){
                    TimerEffect(true);
                    SendWasps(true);
                    waspsAreAttacking = true;
                // }
            }
            if(waspsAreAttacking){
                DecrementFuel();
                if(fuel<=0){
                    TimerEffect(false);
                    SendWasps(false);
                }
            }
        }
        
    }
    private void DecrementFuel(){
        fuel -= (Time.deltaTime / fuelTimer);
        waspsTimer.fillAmount = fuel;
        return;
    }
    private void TougleAimCheck(bool aimToggle){
        if(aimToggle){
            TimerEffect(false);
            SendWasps(false);
            canUseWasps = !canUseWasps;
            AimEffect(canUseWasps);
        }
    }
    private void SendWasps(bool b){
        wasps.SetActive(b);
    }
    private bool MouseClick(int i){
        return Input.GetMouseButtonDown(i);
    }
    private bool CancelAimButton(){
        return Input.GetKey(cancelAimButton);
    }
    private bool MouseHold(int i){
        return Input.GetMouseButton(i);
    }
    private bool MouseRelease(int i){
        return Input.GetMouseButtonUp(i);
    }
    private void SetAttackPosition(){
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;
        if(groundPlane.Raycast(cameraRay, out rayLenght)){
            Vector3 pointToLook = (cameraRay.GetPoint(rayLenght));
            Vector3 finalPoint = new Vector3 (pointToLook.x,  0.3f  , pointToLook.z);
            WaspsHolderPosition(finalPoint);
            return;
        }
        Debug.LogWarning("impossivel atacar aqui. nao há chao");
    }
    private void WaspsHolderPosition(Vector3 pos){
        waspsHolder.transform.position = new Vector3 (pos.x, 0.3f, pos.z);
    }


    private void TimerEffect(bool active){
        waspsTimer.gameObject.SetActive(active);
    }

    private void AimEffect(bool active){
        waspsAimEffect.SetActive(active);
    }
    private void AttackingUI(){
        AimEffect(false);
        TimerEffect(true);
    }
    private void AimingUI(){
        TimerEffect(false);
        AimEffect(true);
    }
}
