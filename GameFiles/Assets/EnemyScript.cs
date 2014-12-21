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
		string name=c.gameObject.name;
		if(!isDead&&name!="Enemy"){
			print("Enemy Hit by "+name);
	    	Instantiate(explosion,transform.position,transform.rotation);
	    	scoreHandler.EnemyDestroyed(1,0.1f);
        	GameObject.Destroy(gameObject);
        	isDead=true;
    	}	
	}
}
