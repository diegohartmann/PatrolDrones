using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void Load(string sceneName, float time){
        StartCoroutine(_Load(sceneName, time));
    }

    private IEnumerator _Load(string sceneName, float time){
        yield return new WaitForSeconds (time);
        SceneManager.LoadScene(sceneName);
    }
}
