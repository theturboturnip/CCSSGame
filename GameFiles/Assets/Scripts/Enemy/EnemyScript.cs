using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    bool isDead=false;
    ScoreHandlerScript scoreHandler;
    GameObject model;
    public int health=1;
	// Use this for initialization
	void Start () {
		model=transform.GetChild(0).gameObject;
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
		//rigidbody.velocity=transform.forward;
	}
	void Update(){
		//transform.position=new Vector3(transform.position.x,0f,transform.position.z);
		//rigidbody.velocity=Vector3.zero;
		if(Camera.main.WorldToScreenPoint(transform.position).x<0) GameObject.Destroy(gameObject);
	}
	public void OnCollisionEnter (Collision c) {
		if(c.gameObject.tag!="Enemy"){
			if(c.gameObject.tag=="Bullet"){
				BulletScript bullet = c.gameObject.GetComponent<BulletScript>();
				if((bullet.Shooter==gameObject&&bullet.ticks>=90))
					return;
			}
			getHurt(1,c);
    	}	
	}
	public void OnCollisonStay (Collision c){
		if(c.gameObject.tag!="Enemy"){
			transform.Translate(Vector3.back);
		}
	}
	public void getHurt(int value,Collision c){
		if(!isDead) health-=value;
		string causeOfDeath="";
		//if(c.gameObject.tag!=null) causeOfDeath=c.gameObject.tag;
		//if(c.gameObject.tag=="Explosion") causeOfDeath="Explosion";
		if(c.gameObject.tag=="Bullet"){	
			if(c.gameObject.GetComponent<BulletScript>().Shooter.tag=="Enemy") causeOfDeath="Friendly";
			else causeOfDeath="Bullet";
		}
		if(health<=0) die(causeOfDeath); 
	}
	void die(string causeOfDeath){
		GameObject Explosion;
		Explosion=Instantiate(explosion,transform.position,transform.rotation) as GameObject;
		/*if(causeOfDeath=="Friendly")
			Explosion.tag="Friendly";*/
	    float multiplier=0.1f;
	    int worth=1;
	    if(causeOfDeath=="Explosion") multiplier=0.2f;
	    if(causeOfDeath=="Friendly"){
	    	multiplier=0.0f;
	    	worth=0;
	    }
	    scoreHandler.EnemyDestroyed(worth,multiplier);
        GameObject.Destroy(gameObject);
        isDead=true;
	}
}
