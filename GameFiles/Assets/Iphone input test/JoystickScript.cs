using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class JoystickScript : MonoBehaviour {
	public GameObject joystickImage;
	public string horizontalAxis="Horizontal",verticalAxis="Vertical";
	public Vector2 axes;
	public bool Analog=false;
	RectTransform joystickBounds;
	ScrollRect joystickRect;
	JoystickImageScript joystickButton;
	Vector2 deadZone;
	BaseEventData e=new BaseEventData(EventSystem.current);
	// Use this for initialization
	void Start () {
		joystickBounds=joystickImage.GetComponent<RectTransform>();
		joystickRect=joystickImage.GetComponent<ScrollRect>();
		joystickButton=joystickImage.GetComponent<JoystickImageScript>();
		deadZone=joystickRect.content.anchoredPosition;
	}
	float round(float toRound){
		float toreturn=0;
		if(toRound<-0.5)
			toreturn=-1;
		else if(toRound>0)
			toreturn=1;
		return toreturn;
	}
	// Update is called once per frame
	void Update () {
		float radius=32f;
		// Calculate the offset vector from the center of the circle to our position
     	Vector2 offset = joystickBounds.anchoredPosition - deadZone;
     	float distance = offset.magnitude;

     	axes=offset;
     	if(distance!=0&&!Analog){
     		//axes=Vector2.zero;
     		axes*=radius/distance;
     	}
     	axes.Normalize();
     	InputWrapper.SetAxis(horizontalAxis,axes.x);
     	InputWrapper.SetAxis(verticalAxis,axes.y);
     	// Calculate the linear distance of this offset vector
     	
     	if (radius < distance)
     	{
        	// If the distance is more than our radius we need to clamp
          	// Calculate the direction to our position
          	Vector2 direction = offset / distance;
          	// Calculate our new position using the direction to our old position and our radius
          	joystickBounds.anchoredPosition = deadZone + direction * radius;
     	}
     	if(joystickButton.IsHighlighted(e)){
     		joystickBounds.anchoredPosition=deadZone;
     		InputWrapper.SetAxis(horizontalAxis,0.0f);
     		InputWrapper.SetAxis(verticalAxis,0.0f);
     	}
	}
}
