using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    bool isDead=false;
    ScoreHandlerScript scoreHandler;
	// Use this for initialization
	void Start () {
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
	}
	public void OnCollisionEnter (Collision c) {
		if(!isDead){
	    	Instantiate(explosion,transform.position,transform.rotation);
	    	float multiplier=0.1f;
	    	if(c.gameObject.tag=="Explosion") multiplier=0.2f;
	    	scoreHandler.EnemyDestroyed(1,multiplier);
        	GameObject.Destroy(gameObject);
        	isDead=true;
    	}	
	}
}
