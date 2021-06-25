using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawDroneInstantiator : MonoBehaviour
{
    [SerializeField]private bool instantiateAtAwake = false;
    [SerializeField]private GameObject strawPanelPrefab = null;
    [SerializeField]private Transform strawPanelPos = null;
    [SerializeField]private GameObject strawDronePrefab = null;
    
    private void Awake() {
        InstantiateNewStrawPannel();
        if(instantiateAtAwake){
            FillStrawDrones();
        }
    }
    public void FillStrawDrones(){
        foreach (Transform pos in transform){
            if(pos.childCount == 0){
                CreateStrawDroneIn(pos);
            }
        }
    }
    public void InstantiateNewStrawPannel(){
        Instantiate(strawPanelPrefab, strawPanelPos.position, strawPanelPos.rotation);
    }
    private void CreateStrawDroneIn(Transform _pos){
        Instantiate(strawDronePrefab, _pos.position, _pos.rotation, _pos);
    }
}
