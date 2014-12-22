using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    bool isDead=false;
    ScoreHandlerScript scoreHandler;
    string[] enemyNames={};
	// Use this for initialization
	void Start () {
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
	}
	void Update(){
		rigidbody.velocity=Vector3.zero;
	}
	public void OnCollisionEnter (Collision c) {
		if(!isDead&&c.gameObject.tag!="Enemy"){
			print(c.gameObject.name);
	    	Instantiate(explosion,transform.position,transform.rotation);
	    	float multiplier=0.1f;
	    	if(c.gameObject.tag=="Explosion") multiplier=0.2f;
	    	scoreHandler.EnemyDestroyed(1,multiplier);
        	GameObject.Destroy(gameObject);
        	isDead=true;
    	}	
	}
	public void OnCollisonStay (Collision c){
		if(c.gameObject.tag!="Enemy"){
			transform.Translate(Vector3.back);
		}
	}
}
