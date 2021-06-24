using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinionTriggerChecker : MonoBehaviour
{
    private const int triggerLayer = 12;
    private const int loadLevelDelay = 0;
    [SerializeField] private UnityEvent OnMinionTriggerReached = null;
    private SceneLoader loader;
    public void TriggerCheckerInit() {
        loader = FindObjectOfType<SceneLoader>();
    }
    private void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;
        int objLayer = obj.layer;
        if(objLayer == triggerLayer){
            OnMinionTriggerReached.Invoke();
        }
    }
    public void FinishLevel(){
        int nextLevelIndex = loader.CurrentIndex() + 1;
        loader.Load(nextLevelIndex, loadLevelDelay);
    }
}