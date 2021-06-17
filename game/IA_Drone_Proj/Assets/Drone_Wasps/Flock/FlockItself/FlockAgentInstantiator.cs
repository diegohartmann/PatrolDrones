using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgentInstantiator : MonoBehaviour
{
    [SerializeField] Transform WaspsHolder = null;
    [SerializeField] FlockStayInRadiusBehaviour radiusBehaviour = null;
    private void Awake() {
        radiusBehaviour.center = WaspsHolder;
        CenterPos(transform);
        CenterPos(WaspsHolder);
    }
    private void CenterPos(Transform _transform){
        _transform.position = new Vector3(0, 0.3f, 0);
    }
}
