using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void Load(string _sceneName, float _time){
        StartCoroutine(_Load(_sceneName, _time));
    }
    public void Load(int _index, float _time){
        StartCoroutine(_Load(_index, _time));
    }
    public void Load(string _sceneName){
        StartCoroutine(_Load(_sceneName, 0));
    }
    public void Reload(float _time){
        StartCoroutine(_Reload(_time));
    }
    private IEnumerator _Load(string _sceneName, float _time){
        yield return new WaitForSeconds (_time);
        SceneManager.LoadScene(_sceneName);
    }
    private IEnumerator _Load(int _sceneIndex, float _time){
        yield return new WaitForSeconds (_time);
        SceneManager.LoadScene(_sceneIndex);
    }
    private IEnumerator _Reload(float time){
        yield return new WaitForSeconds (time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int CurrentIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }
}
