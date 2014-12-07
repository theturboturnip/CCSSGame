using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision c) {
	     Instantiate(explosion,transform.position,transform.rotation);
         GameObject.Destroy(gameObject);
	}
}
