using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void Load(string sceneName, float time){
        StartCoroutine(_Load(sceneName, time));
    }
    public void Load(int index, float time){
        StartCoroutine(_Load(index, time));
    }
    public void Load(string sceneName){
        StartCoroutine(_Load(sceneName, 0));
    }
    public void Reload(float time){
        StartCoroutine(_Reload(time));
    }
    private IEnumerator _Load(string sceneName, float time){
        yield return new WaitForSeconds (time);
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator _Load(int sceneIndex, float time){
        yield return new WaitForSeconds (time);
        SceneManager.LoadScene(sceneIndex);
    }
    private IEnumerator _Reload(float time){
        yield return new WaitForSeconds (time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int CurrentIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }
}
