using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaspAttack : MonoBehaviour{
    [SerializeField] private bool infiniteFuel = false;
    [SerializeField] private GameObject waspsHolder = null;
    [SerializeField] private GameObject wasps = null;
    [SerializeField] private Image waspsTimer = null;
    [SerializeField] private GameObject waspsAimEffect = null;
    [SerializeField] private KeyCode cancelAimButton = KeyCode.C;
    [SerializeField][Range(0,1)]private float fuel = 1;
    [SerializeField] private float fuelTimer = 3;
    [SerializeField] private Color waspChargerColor = Color.yellow;
    [SerializeField] private Color waspChargerColorFull = Color.green;
    [SerializeField] private Color waspEmptyColor = Color.black;
    [Header("Wasp Bomb")]
    [SerializeField] private GameObject throwerDrone = null;
    [SerializeField] private Transform WaspBombHolder = null;
    [SerializeField] private GameObject WaspBomb = null;
    private Spin WaspBombSpin = null;
    [SerializeField] private Vector3 BomberThrowerFinalOffset = new Vector3(0,5,0);
    
    private Camera mainCamera;
    private bool canUseWasps = false;
    private bool waspsAreAttacking = false;
    private Transform waspsChargerHolder;
    private Rigidbody WaspBombRB;
    private ThrowerDrone throwerDroneScript;
    [SerializeField] [Range (0,3)] private int initialDeadDronesBars = 0;
    private void Awake() {
        waspsChargerHolder = GameObject.Find("waspsChargerHolder").transform;
        mainCamera = FindObjectOfType<Camera>();
        IncrementDeadDrones(initialDeadDronesBars);
        WaspBombInit();
        ThrowerDroneInit();
    }
    private void ThrowerDroneInit(){
        throwerDroneScript = throwerDrone.GetComponent<ThrowerDrone>(); 
        throwerDroneScript.waspAttack = this;
        ResetThrowerGFX();
    }
    private void WaspBombInit(){
        WaspBomb.GetComponent<WapsBomb>().waspAttack = this;
        WaspBombRB = WaspBomb.GetComponent<Rigidbody>();
        WaspBombSpin = WaspBomb.GetComponent<Spin>();
        ResetBombGFX();
    }
    private void ResetThrowerGFX(){
        throwerDrone.SetActive(false);
        throwerDrone.transform.position = new Vector3 (transform.position.x, 1, transform.position.y);
    }
    public void CheckWapsAttack(){

        if(DronesNetworkComunication.deadDrones < 3 ){
            return;
        }
        if(fuel <= 0){
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
                SendThrowerDrone();
            }
            if(waspsAreAttacking && !infiniteFuel){
                FuelUpdate();
            }
        }
    }
    public void FillFuel(){
        if(fuel!=1){
            fuel = 1;
        }
    }
    private void FuelUpdate(){
        DecrementFuel();
        if(fuel<=0){
            TimerEffect(false);
            SendWasps(false);
            IncrementDeadDrones(-DeadDrones());
        }
    }
    public void BlowBomb(){
        //chamado no script "Waspbomb" da bomba
        ResetBombGFX();
        AimEffect(false);
        TimerEffect(true);
        SendWasps(true);
        waspsAreAttacking = true;
    }
    private void ResetBombGFX(){
        VisibleBomb(false);
        WaspBombRB.isKinematic = true;
        WaspBomb.transform.parent = WaspBombHolder.transform; 
        Center(WaspBomb.transform);
        WaspBombSpin.enabled=false;
    }
    private void SendThrowerDrone(){
        ResetThrowerGFX();
        throwerDrone.SetActive(true);
    }
    public void ThrowBomb(){
        WaspBomb.transform.parent = null;
        WaspBombRB.isKinematic = false;
        WaspBombSpin.enabled = true;
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
            img.color = waspEmptyColor;
            if(i+1 <= DeadDrones()){
                img.color = waspChargerColor;
                charge ++;
            }
        }
        if(charge >= 3){
            foreach (Transform item in waspsChargerHolder){
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
            VisibleBomb(canUseWasps);
            Center(WaspBomb.transform);
            WaspBombRB.isKinematic = true;
            throwerDrone.SetActive(false);
        }
    }
    private void SendWasps(bool b){
        foreach(Transform wasp in wasps.transform){
            Center(wasp);
        }
        wasps.SetActive(b);
    }
    private void Center(Transform _transform){
        _transform.localPosition = new Vector3(0,0,0);
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
            throwerDroneScript.finalPos = finalPoint + BomberThrowerFinalOffset;
            return;
        }
        Debug.LogWarning("impossivel atacar aqui: nao há chao");
    }
    private void VisibleBomb(bool visible){
        WaspBomb.SetActive(visible);
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
