using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]

public class MoverScript : MonoBehaviour {
    public bool CameraRelative=false;
	public float  MoveSpeed=5f;
	public Transform Bullet;
    //Rigidbody rigidbody;
   	// Use this for initialization
	void Start () {
	   //rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!CameraRelative) {
            rigidbody.AddTorque(transform.TransformDirection(Vector3.up)*Input.GetAxis("Horizontal")*0.5f);
            rigidbody.AddForce(transform.TransformDirection(Vector3.forward)*MoveSpeed*Input.GetAxis("Vertical"));
			Vector3 bulletSpawn=transform.position+transform.forward;
			bulletSpawn.y-=1;
			if(Input.GetButtonDown("Fire1")) 
				Instantiate(Bullet,bulletSpawn,transform.rotation);
		}else{
		
		}
	}
	Vector3 noY(Vector3 toMod){
		Vector3 toReturn = toMod;
		toReturn.y = 0;
		return toReturn;
	}
}
