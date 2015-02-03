using UnityEngine;
using UnityEditor;
using System.Collections;

public class MovementScriptPhone : MovementScript {
    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }
    void Update () {
        moveIfRequired();
        //shootIfRequired();
        rigidbody.velocity=Vector3.zero;
        if (isDead){
            isDead=false;
            health=10;
        }
    }  

    public virtual void moveIfRequired(){
        Vector3 mouseWorldPos=Input.mousePosition-new Vector3(16,16,0);
        //transform.LookAt(ScreenToWorldPoint(mouseWorldPos));
        print(InputWrapper.GetAxis("Horizontal2"));
        transform.rotation=Quaternion.Euler(new Vector3(InputWrapper.GetAxis("Horizontal2"),0,InputWrapper.GetAxis("Vertical2")));
        Vector3 movementVector=new Vector3(InputWrapper.GetAxis("Horizontal1"),0,InputWrapper.GetAxis("Vertical1"))*playerSpeed*Time.deltaTime;
        transform.position+=movementVector;

        //Checking if the player is offscreen
        
        /*if(xOff)
            if()
            transform.position=new Vector3(transform.position.x-movementVector.x,transform.position.y,transform.position.z);
        if(yOff)
            transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z-movementVector.z);
    */}
}
