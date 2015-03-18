using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		Ray r=Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r,out hit)){
			target=hit.transform.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateTowards(target.transform.position,5.0f);
		transform.Translate(Vector3.forward*Time.deltaTime);
	}
}
