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
    private void Awake() {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void CheckWapsAttack(){
        TougleAimCheck(MouseClick(1));
        if(canUseWasps){
            OtherClicksCheck(MouseHold(1), MouseRelease(1));
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
    private void OtherClicksCheck(bool _aiming, bool _releasedAim){
        if(_aiming){
            SetAttackPosition();
            TougleAimCheck(CancelAimButton());
            return;
        }
        if(_releasedAim){
            AimEffect(false);
            TryToAttack();
            return;
        }
        return;
    }
    private void TryToAttack(){
        TimerEffect(true);
        SendWasps(true);
        return;
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

    private void UpdateTimerUI(float amt){
        waspsTimer.fillAmount = amt;
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
