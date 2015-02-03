using UnityEngine;
using System.Collections;

public class MovingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horiz=InputWrapper.GetAxis("Horizontal1"),vert=InputWrapper.GetAxis("Vertical1");
		Vector3 toTranslate=new Vector3(horiz,0f,vert)*Time.deltaTime;
		transform.Translate(toTranslate);
	}
}
