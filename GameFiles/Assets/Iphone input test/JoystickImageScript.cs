using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class JoystickImageScript : Selectable {
	public bool selected=false;
	public new bool IsHighlighted(BaseEventData e){
		return base.IsHighlighted(e);
	}
}
