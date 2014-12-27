using UnityEngine;
using System.Collections;

public class BulletScript2 : MonoBehaviour {
	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
		line.enabled=true;
		//Ray ray=new Ray(transform.position,transform.forward);//new Ray(transform.position, transform.rotation.eulerAngles);
		line.SetPosition(0,transform.GetChild(1).position);
		line.SetPosition(1,transform.GetChild(2).position);
	}
	public void SetTarget(Vector3 target){
		line.SetPosition(1,target);
	}
}
