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
        instantiator.InstantiateNewStrawPannel();
        instantiator.FillStrawDrones();
    }
}
