using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
	float scale=0.1f;
	SphereCollider myCollider;
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponent<SphereCollider>();
		myCollider.radius=0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		print(myCollider.radius);
		if(myCollider.radius<1f)
		myCollider.radius+=scale;

	}
}
