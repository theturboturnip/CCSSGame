using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Transform startExplosion,endExplosion;
	public float ticks=0;
    GameObject model;
    Vector3 direction;
    LineRenderer line;
	// Use this for initialization
	void Start () {
        //model=transform.GetChild(0).gameObject;
        line=gameObject.GetComponent<LineRenderer>();
        line.enabled=false;
		if(startExplosion!=null)
		Instantiate(startExplosion, transform.position, transform.rotation);
		direction=transform.TransformDirection(Vector3.forward*75f);	
		//rigidbody.AddForce(direction);
	}
	void End (int explode) {
		if(endExplosion!=null&&explode==1)
		Instantiate(endExplosion, transform.position, transform.rotation);
		GameObject.Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		rigidbody.velocity=direction;
		line.SetPosition(0,transform.position-transform.forward*0.5f);
        line.SetPosition(1,transform.position+transform.forward*0.5f);
        line.enabled=true;
        Ray ray=new Ray(transform.position,transform.forward);

		//if(!model.renderer.isVisible)
        //	End(0);
	}
	void OnCollisionEnter(Collision c){
		string name=c.gameObject.name;
		//print(name);
		if(c.gameObject.tag!="Bullet"&&c.gameObject.tag!="Explosion"&&c.gameObject.tag!="Player")
		End(1);
	}
}
