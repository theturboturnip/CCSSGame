using UnityEngine;
using System.Collections;

public class BorgUIScript : MonoBehaviour {
	float timer=5.0f,currentTime=0f,maxMag=10.0f;
	// Use this for initialization
	void Start () {
	}
	void Update(){
		currentTime-=Time.deltaTime;
		if (currentTime<0){
			rigidbody.angularVelocity*=0.5f;
			rigidbody.AddTorque(RandVec(0.0f,maxMag));
			currentTime=timer;
		}
		
	}
	// Update is called once per frame
	Vector3 RandVec (float bottom=0.0f,float top=360.0f) {
		return new Vector3(0.0f,Random.Range(bottom,top),Random.Range(bottom,top));
	}
}
