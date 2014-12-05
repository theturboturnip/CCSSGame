using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	AudioListener listener;
	// Use this for initialization
	void Start () {
		listener = gameObject.GetComponents<AudioListener> ()[0];
	}
	
	// Update is called once per frame
	void Update () {
		GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		if(mainCamera==gameObject){
			listener.enabled=true;
		}else{
			listener.enabled=false;
		}
	}
}
