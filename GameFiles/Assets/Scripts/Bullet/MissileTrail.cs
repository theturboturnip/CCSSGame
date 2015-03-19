using UnityEngine;
using System.Collections;

public class MissileTrail : MonoBehaviour {
	float lifeSpan=1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent==null)
			lifeSpan-=Time.deltaTime;
		if(lifeSpan<=0.0f)
			Destroy(gameObject);
	}
}
