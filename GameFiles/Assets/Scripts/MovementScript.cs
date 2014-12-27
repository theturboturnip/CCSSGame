using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform model,bullet;
    public Vector3 bossLocation=Vector3.zero,CameraLimit=Vector3.zero;
    public float cameraSpeed,playerSpeed;
    public bool moveInfinitely=false;
    float bulletTicks,bulletTickLimit=10;

	// Update is called once per frame
	void Update () {
	    moveIfRequired();
        shootIfRequired();
        CameraLimit=ScreenToWorldPoint(new Vector3(Screen.width*1.5f,Screen.height/2,0));
    }  

    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }

    void shootIfRequired(){
        if(Input.GetMouseButton(0)&&bulletTicks>=bulletTickLimit){
            GameObject Bullet;
            Bullet = Instantiate(bullet,model.position+model.TransformDirection(Vector3.forward),model.rotation) as GameObject;
            //print(Bullet);
            //BulletScript2 script=Bullet.GetComponent<BulletScript2>();
            //script.SetTarget(ScreenToWorldPoint(Input.mousePosition));
            bulletTicks=0;
         }
         bulletTicks++;
    }

    void moveIfRequired(){
        if(moveInfinitely) transform.Translate(Vector3.right*cameraSpeed*Time.deltaTime);
        else transform.position=Vector3.MoveTowards(transform.position,bossLocation,cameraSpeed*Time.deltaTime);
        Vector3 mouseWorldPos=Input.mousePosition;
        model.LookAt(ScreenToWorldPoint(mouseWorldPos));
        Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*playerSpeed*Time.deltaTime;
        model.position+=movementVector;

        //Checking if the player is offscreen
        Vector3 screenPos=WorldToScreenPoint(model.position);    
        bool xOff=!(screenPos.x > 0 && screenPos.x < Screen.width),yOff=!(screenPos.y > 0 && screenPos.y < Screen.height);
        if(xOff)
           model.position=new Vector3(model.position.x-movementVector.x,model.position.y,model.position.z);
        if(yOff)
           model.position=new Vector3(model.position.x,model.position.y,model.position.z-movementVector.z);
    }
}
