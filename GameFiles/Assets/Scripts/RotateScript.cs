using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 euler=transform.rotation.eulerAngles;
		euler.y+=speed*Time.deltaTime;
		transform.rotation=Quaternion.Euler(euler);
	}
}
