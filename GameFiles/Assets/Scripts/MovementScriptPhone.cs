using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class MovementScriptPhone : MovementScript {
    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }

    public override void rotateIfRequired(){
        float yRot=(float)(Math.Atan2((double)InputWrapper.GetAxis("Horizontal2"),(double)InputWrapper.GetAxis("Vertical2"))* (180/Math.PI));
        transform.rotation=Quaternion.Euler(new Vector3(0,yRot,0));
    }
    public override void shootIfRequired(){
        if(InputWrapper.GetAxis("Horizontal2")!=0||InputWrapper.GetAxis("Vertical2")!=0){
            print("Attempting Shoot");  
            foreach(BulletSpawnerScript b in GetComponentsInChildren<BulletSpawnerScript>()){
                b.shoot();
            }
        }
    }
}
