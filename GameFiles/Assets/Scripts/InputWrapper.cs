using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InputWrapper : MonoBehaviour {
	static Dictionary<string,float> axes = new Dictionary<string, float>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static float GetAxis(string name){
		if(axes.ContainsKey(name))
			return axes[name];
		else 
			return Input.GetAxis(name);
	}
	public static void SetAxis(string name,float value){
		if(axes.ContainsKey(name))
			axes[name]=value;
		else
			axes.Add(name,value);
	}
}
