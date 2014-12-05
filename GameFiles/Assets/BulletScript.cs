using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Transform startExplosion,endExplosion;
	public float ticks=0;
	// Use this for initialization
	void Start () {
		if(startExplosion!=null)
		Instantiate (startExplosion, transform.position, transform.rotation);
	}
	void End () {
		if(endExplosion!=null)
		Instantiate (endExplosion, transform.position, transform.rotation);
		GameObject.Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward*Time.deltaTime*100f);	
		ticks += 1;
		if (ticks >= 500)
						End ();
	}
	void OnCollisionEnter(Collision collision){
		End ();
	}
}
