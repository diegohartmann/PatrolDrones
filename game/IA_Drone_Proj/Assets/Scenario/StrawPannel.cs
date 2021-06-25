using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawPannel : MonoBehaviour
{
    private StrawDroneInstantiator instantiator;
    private void OnEnable() {
        instantiator = FindObjectOfType<StrawDroneInstantiator>();
    }
    public void InstantiateAnotherPanel(){
        if(instantiator == null){
            instantiator = FindObjectOfType<StrawDroneInstantiator>();
        }
        instantiator.InstantiateNewStrawPannel();
        instantiator.FillStrawDrones();
    }
}
