using UnityEngine;
using System.Collections;
[RequireComponent (typeof (CharacterController))]

public class BulletScript : MonoBehaviour {
	public Transform startExplosion,endExplosion;
	public float ticks=0;
	CharacterController controller;
    GameObject model;
	// Use this for initialization
	void Start () {
		controller=gameObject.GetComponent<CharacterController>();
        model=transform.GetChild(0).gameObject;
		if(startExplosion!=null)
		Instantiate(startExplosion, transform.position, transform.rotation);
		Vector3 direction=transform.TransformDirection(Vector3.forward*100f);	
		//rigidbody.AddForce(direction);
		rigidbody.velocity=direction;
	}
	void End (int explode) {
		if(endExplosion!=null&&explode==1)
		Instantiate(endExplosion, transform.position, transform.rotation);
		GameObject.Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		//controller.Move(direction);
		if(!model.renderer.isVisible)
        	End(0);
	}
	void OnCollisionEnter(Collision collision){
		string name=collision.gameObject.name;
		print("Bullet Collided With "+name);
		if(name!="Bullet(Clone)")
		End(1);
	}
}
