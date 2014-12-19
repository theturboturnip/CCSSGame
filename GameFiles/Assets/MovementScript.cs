using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform model,bullet;
    public Vector3 bossLocation=Vector3.zero;
    public float cameraSpeed,playerSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	     transform.position=Vector3.MoveTowards(transform.position,bossLocation,cameraSpeed*Time.deltaTime);
         Vector3 mouseWorldPos=Input.mousePosition;
         mouseWorldPos.z=12;
         mouseWorldPos=Camera.main.ScreenToWorldPoint(mouseWorldPos);
         model.LookAt(mouseWorldPos);
         Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
         print(movementVector);
         model.position+=movementVector*playerSpeed*Time.deltaTime;
         if(Input.GetMouseButton(0)){
            Instantiate(bullet,model.position,model.rotation);
         }
    }
    
}
