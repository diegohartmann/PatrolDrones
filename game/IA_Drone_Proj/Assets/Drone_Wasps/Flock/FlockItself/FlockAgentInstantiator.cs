using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgentInstantiator : MonoBehaviour
{
    [SerializeField] private Transform WaspsHolder = null;
    [SerializeField] private FlockStayInRadiusBehaviour radiusBehaviour = null;
    private void Awake() {
        radiusBehaviour.center = WaspsHolder;
        CenterPos(transform);
        CenterPos(WaspsHolder);
    }
    private void CenterPos(Transform _transform){
        _transform.position = new Vector3(0, 0.3f, 0);
    }
}
