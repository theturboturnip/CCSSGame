using UnityEngine;
using System.Collections;

public class DeviceHandlerScript : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Start () {
		#if UNITY_EDITOR
		unityEditor();
		#endif
		#if UNITY_IPHONE
		unityPhone();
		#endif
		#if UNITY_ANDROID
		unityPhone();
		#endif
		#if UNITY_STANDALONE
		unityStandalone();
		#endif
	}
	void unityEditor(){
		//GameObject.Find("Handlers/UIHandler/Canvas/Dual Joystick Setup").setActive(false);
	}
	void unityPhone(){
		Screen.lockCursor=true;
		GameObject.Find("Handlers/UIHandler/Canvas/Dual Joystick Setup").SetActive(true);
	}
	void unityStandalone(){
		GameObject.Find("Handlers/UIHandler/Canvas/Dual Joystick Setup").SetActive(false);
	}
}
