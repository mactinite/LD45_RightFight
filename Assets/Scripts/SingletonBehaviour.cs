using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  SingletonBehavior: MonoBehaviour
{
    public static SingletonBehavior _instance = null;

    void Awake(){
        if(_instance == null){
            _instance = this;
        } else {
            Destroy(this);
        }
    }
}
