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
	}
	void End () {
		if(endExplosion!=null)
		Instantiate(endExplosion, transform.position, transform.rotation);
		GameObject.Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		Vector3 direction=transform.TransformDirection(Vector3.forward*Time.deltaTime*100f);	
		controller.Move(direction);
		if(!model.renderer.isVisible)
        	End();
	}
	void OnCollisionEnter(Collision collision){
		End();
	}
}
