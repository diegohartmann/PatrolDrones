using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatinTest : MonoBehaviour
{
    public Animator anim;
    void Update()
    {
        if(Input.GetKey(KeyCode.O)){
            anim.SetTrigger("Open");
        }
        if(Input.GetKey(KeyCode.C)){
            anim.SetTrigger("Close");
        }
    }
}
