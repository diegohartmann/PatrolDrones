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
    [SerializeField] private Color waspChargerColor = Color.yellow;
    [SerializeField] private Color waspChargerColorFull = Color.green;
    [SerializeField] private Color waspEmptyColor = Color.black;
    private Transform waspsChargerHolder;
    private void Awake() {
        waspsChargerHolder = GameObject.Find("waspsChargerHolder").transform;
        mainCamera = FindObjectOfType<Camera>();
        IncrementDeadDrones(0);
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
                TimerEffect(true);
                SendWasps(true);
                waspsAreAttacking = true;
                
            }
            if(waspsAreAttacking){
                DecrementFuel();
                if(fuel<=0){
                    TimerEffect(false);
                    SendWasps(false);
                    IncrementDeadDrones(-DeadDrones());
                }
            }
        }
    }
    private void DecrementFuel(){
        fuel -= (Time.deltaTime / fuelTimer);
        waspsTimer.fillAmount = fuel;
        return;
    }
    public void IncrementDeadDrones(int amt){
        DronesNetworkComunication.deadDrones += amt;
        int charge = 0;
        for (int i = 0; i < waspsChargerHolder.childCount; i++){
            Image img = waspsChargerHolder.GetChild(i).GetComponent<Image>();
            // img.color = (i <= DeadDrones())? waspChargerColor : waspEmptyColor; 
            img.color = waspEmptyColor;
            if(i+1 <= DeadDrones()){
                img.color = waspChargerColor;
                charge ++;
                // return;
            }
        }
        if(charge >= 3){
            foreach (Transform item in waspsChargerHolder)
            {
                item.GetComponent<Image>().color = waspChargerColorFull;
            }
        }
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
    private int DeadDrones(){
        return DronesNetworkComunication.deadDrones;
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
