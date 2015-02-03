using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class JoystickScript : MonoBehaviour {
	public GameObject joystickImage;
	public string horizontalAxis="Horizontal",verticalAxis="Vertical";
	RectTransform bounds,joystickBounds;
	ScrollRect joystickRect;
	JoystickImageScript joystickButton;
	Image myImg;
	Vector2 deadZone;
	Vector2 previousPos;
	BaseEventData e=new BaseEventData(EventSystem.current);
	// Use this for initialization
	void Start () {
		myImg=GetComponent<Image>();
		joystickBounds=joystickImage.GetComponent<RectTransform>();
		joystickRect=joystickImage.GetComponent<ScrollRect>();
		joystickButton=joystickImage.GetComponent<JoystickImageScript>();
		deadZone=joystickRect.content.anchoredPosition;
		previousPos=joystickRect.normalizedPosition;
		bounds=GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		float radius=32f;
		// Calculate the offset vector from the center of the circle to our position
     	Vector2 offset = joystickBounds.anchoredPosition - deadZone;
     	Vector2 axes=offset;
     	axes.Normalize();

     	InputWrapper.SetAxis(horizontalAxis,axes.x);
     	InputWrapper.SetAxis(verticalAxis,axes.y);
     	// Calculate the linear distance of this offset vector
     	float distance = offset.magnitude;
     	if (radius < distance)
     	{
        	// If the distance is more than our radius we need to clamp
          	// Calculate the direction to our position
          	Vector2 direction = offset / distance;
          	// Calculate our new position using the direction to our old position and our radius
          	joystickBounds.anchoredPosition = deadZone + direction * radius;
     	}
     	if(joystickButton.IsHighlighted(e)){
			distance=Vector2.Distance(joystickBounds.anchoredPosition,deadZone);
			float speed=distance/10;
			if(speed<1) speed=1;
			joystickBounds.anchoredPosition=Vector2.MoveTowards(joystickBounds.anchoredPosition,deadZone,speed);
			//Color c=new Color(255,255,255,axes.magnitude);
			//myImg.color=c;
		}else{
			//Color c=myImg.color;
			//c.a=0.0f;
			//myImg.color=c;
		}
	}
}
