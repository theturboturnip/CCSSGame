using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickScript : MonoBehaviour {
	public GameObject joystickImage;
	RectTransform bounds,joystickBounds;
	ScrollRect joystickRect;
	Selectable joystickbutton;
	Vector2 deadZone;
	Vector2 previousPos;
	// Use this for initialization
	void Start () {
		joystickBounds=joystickImage.GetComponent<RectTransform>();
		joystickRect=joystickImage.GetComponent<ScrollRect>();
		joystickbutton=joystickImage.GetComponent<Selectable>();
		deadZone=joystickRect.content.anchoredPosition;
		previousPos=joystickRect.normalizedPosition;
		bounds=GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance=Vector2.Distance(joystickBounds.anchoredPosition,deadZone);
		float speed=distance/10;
		if(speed<1) speed=1;
		joystickBounds.anchoredPosition=Vector2.MoveTowards(joystickBounds.anchoredPosition,deadZone,speed);
	}
}
