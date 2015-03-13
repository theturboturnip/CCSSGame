using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorgButton : Button {
	RectTransform r,p;
	Vector3 center;
	LineRenderer l;
	void Start(){
		p=transform.parent.gameObject.GetComponent<RectTransform>();
		r=GetComponent<RectTransform>();
		l=GetComponent<LineRenderer>();
		center=Camera.main.ViewportToWorldPoint(Vector3.one*0.5f);
		center.z=0.0f;
		l.SetPosition(1,center);
//new Vector3(p.pivot.x*p.rect.width,p.pivot.y*p.rect.height,0.0f);
	}
	void Update(){
		l.SetPosition(0,Camera.main.ViewportToWorldPoint(r.rect.center));
		r.rotation=Quaternion.Euler(Vector3.zero);//-p.eulerAngles);
		//transform.LookAt(Camera.mainCamera.transform);
	}
}
