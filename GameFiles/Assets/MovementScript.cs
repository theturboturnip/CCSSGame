using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform model,bullet;
    public Vector3 bossLocation=Vector3.zero;
    public float cameraSpeed,playerSpeed;
    float bulletTicks,bulletTickLimit=10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	     transform.position=Vector3.MoveTowards(transform.position,bossLocation,cameraSpeed*Time.deltaTime);
         Vector3 mouseWorldPos=Input.mousePosition;
         model.LookAt(ScreenToWorldPoint(mouseWorldPos));
         Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
         model.position+=movementVector*playerSpeed*Time.deltaTime;
         if(Input.GetMouseButton(0)&&bulletTicks>=bulletTickLimit){
            Instantiate(bullet,model.position+model.TransformDirection(Vector3.forward)*1.5f,model.rotation);
            bulletTicks=0;
         }
         bulletTicks++;
    }
    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }
    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }
}
