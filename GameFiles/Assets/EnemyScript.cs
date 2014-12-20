using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    ScoreHandlerScript scoreHandler;
	// Use this for initialization
	void Start () {
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
	}
	void OnCollisionEnter (Collision c) {
		print("EnemyDestroyed");
	    Instantiate(explosion,transform.position,transform.rotation);
        GameObject.Destroy(gameObject);
        scoreHandler.EnemyDestroyed(100);
	}
}
