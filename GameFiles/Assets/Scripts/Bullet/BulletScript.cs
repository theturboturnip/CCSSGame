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
		direction=transform.TransformDirection(Vector3.forward*50f);	
	}
	void End (int explode,Vector3 collisionPos) {
		//print(ticks);
		if(endExplosion!=null&&explode==1)
			Instantiate(endExplosion, transform.position, transform.rotation);
		GameObject.Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		ticks--;
		if(ticks<=0) End(0,Vector3.zero);
		transform.Translate(direction*Time.deltaTime,Space.World);
		Ray ray=new Ray(transform.position/*-transform.forward*0.5f*/,transform.forward);
		Vector3 start=ray.GetPoint(0f),end=ray.GetPoint(2f);

		RaycastHit hit;
		Physics.Raycast(ray, out hit, 1.0F);
		if(hit.transform!=null)
			HandleCollision(hit);
		line.SetPosition(0,start);
        line.SetPosition(1,end);
        line.enabled=true;
	}
	void HandleCollision(RaycastHit c){
		GameObject go=c.transform.gameObject;
		bool canCollide=(go!=Shooter&&go!=null)&&go.tag!="Explosion"&&go.layer!=LayerMask.NameToLayer("Powerup");
		if(canCollide){
			if(go.tag=="Enemy") go.GetComponent<EnemyScript>().getHurt(1,go);
			if(go.tag=="Player") go.GetComponent<MovementScript>().getHurt(1);
			End(1,c.point);
		}
	}
	void OnBecameInvisible() {
    	End(0,Vector3.zero);   
    }
}
