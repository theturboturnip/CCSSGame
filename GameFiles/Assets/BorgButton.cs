using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorgButton : Button {
	public void enableChildren(bool toSet){
		foreach(Transform c in transform){
			if(c.gameObject.name!="Right"&&c.gameObject.name!="Left")
				c.gameObject.SetActive(toSet);
		}
	}
}
