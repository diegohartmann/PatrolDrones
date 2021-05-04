using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agenteCollider;
    [HideInInspector]public Collider AgentCollider {get {return agenteCollider;}}
    void Awake()
    {
        agenteCollider = GetComponent<Collider>();        
    }
    public void Move(Vector2 _velocity){
        transform.up = _velocity;
        transform.position += (Vector3)_velocity * Time.deltaTime;
    }

}
