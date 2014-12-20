using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    ScoreHandlerScript scoreHandler;
	// Use this for initialization
	void Start () {
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
	}
	public void OnCollisionEnter (Collision c) {
		print("EnemyHit");
	    Instantiate(explosion,transform.position,transform.rotation);
	    scoreHandler.EnemyDestroyed(1,0.1f);
        GameObject.Destroy(gameObject);
	}
}
