using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgentInstantiator : MonoBehaviour
{
    [SerializeField] GameObject[] FlockPrefab = null;
    [SerializeField] int[] quantity = null;
    [SerializeField] Transform WaspsHolder = null;
    [SerializeField] bool instantiate =false;
    [SerializeField] FlockStayInRadiusBehaviour radiusBehaviour = null;
    private void Awake() {
        radiusBehaviour.center = WaspsHolder;
        CenterPos(transform);
        CenterPos(WaspsHolder);
        if(instantiate){
            CreateWasps();
        }
        WaspsHolder.gameObject.SetActive(false);
    }
    private void CreateWasps() {
        for (int i = 0; i < FlockPrefab.Length; i++){
            for (int j = 0; j < quantity[i]; j++){
                Instantiate(
                    FlockPrefab[i],
                    Random.insideUnitCircle * quantity[i],
                    Quaternion.Euler(Vector3.up * Random.Range(0, 360)),
                    WaspsHolder
                );
            }
        }
    }
    private void CenterPos(Transform _transform){
        _transform.position = new Vector3(0, 0.3f, 0);
    }
}
