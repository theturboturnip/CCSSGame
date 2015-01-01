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
		rigidbody.velocity=transform.forward;
	}
	void Update(){
		transform.position=new Vector3(transform.position.x,0f,transform.position.z);
		//rigidbody.velocity=Vector3.zero;
	}
	public void OnCollisionEnter (Collision c) {
		if(c.gameObject.tag!="Enemy"){
			getHurt(1,c);
    	}	
	}
	public void OnCollisonStay (Collision c){
		if(c.gameObject.tag!="Enemy"){
			transform.Translate(Vector3.back);
		}
	}
	void getHurt(int value,Collision c){
		bool invincible=!model.renderer.isVisible;
		if(!isDead&&!invincible) health-=value;
		if(health<=0) die(c); 
	}
	void die(Collision c){
		Instantiate(explosion,transform.position,transform.rotation);
	    float multiplier=0.1f;
	    if(c.gameObject.tag=="Explosion") multiplier=0.2f;
	    scoreHandler.EnemyDestroyed(1,multiplier);
        GameObject.Destroy(gameObject);
        isDead=true;
	}
}
