using UnityEngine;
using System.Collections;

public class BulletScript2 : MonoBehaviour {
	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled=true;
		//Ray ray=new Ray(transform.position,transform.forward);//new Ray(transform.position, transform.rotation.eulerAngles);
		line.SetPosition(0,transform.position);
		line.SetPosition(1,transform.position+transform.forward);
	}
	public void SetTarget(Vector3 target){
		line.SetPosition(1,target);
	}
}
