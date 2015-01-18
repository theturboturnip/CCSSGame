using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Transform startExplosion,endExplosion;
	public GameObject Shooter;
	public float ticks=100;
    GameObject model;
    Vector3 direction;
    LineRenderer line;
	// Use this for initialization
	void Start () {
        line=gameObject.GetComponent<LineRenderer>();
        line.enabled=false;
		if(startExplosion!=null)
		Instantiate(startExplosion, transform.position, transform.rotation);
		direction=transform.TransformDirection(Vector3.forward*75f);	
	}
	void End (int explode) {
		//print(ticks);
		if(endExplosion!=null&&explode==1)
		Instantiate(endExplosion, transform.position, transform.rotation);
		GameObject.Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		ticks--;
		if(ticks<=0) End(0);
		rigidbody.velocity=direction;
		line.SetPosition(0,transform.position-transform.forward*0.5f);
        line.SetPosition(1,transform.position+transform.forward*0.5f);
        line.enabled=true;
	}
	void OnCollisionEnter(Collision c){
		if((c.gameObject!=Shooter)||(ticks<=90)){
			if(c.gameObject.tag=="Enemy") c.gameObject.GetComponent<EnemyScript>().getHurt(1,c);
			if(c.gameObject.tag=="Player") c.gameObject.GetComponent<MovementScript>().getHurt(1);
			End(1);
		}
	}
}
