using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionTriggerChecker : MonoBehaviour
{
    [SerializeField] private int triggerLayer = 12;
    [SerializeField] private float leadLevelDelay = 1;
    private SceneLoader loader;
    public void TriggerCheckerInit() {
        loader = FindObjectOfType<SceneLoader>();
    }
    private void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;
        int objLayer = obj.layer;
        if(objLayer == triggerLayer){
            LevelComplete();
        }
    }
    private void LevelComplete(){
        int nextLevelIndex = loader.CurrentIndex() + 1;
        loader.Load(nextLevelIndex, leadLevelDelay);
    }
}