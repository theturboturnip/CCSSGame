using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorgHealth : MonoBehaviour {
	public Color color1,color2;
	RectTransform t;
	Image i;
	float maxWidth;
	// Use this for initialization
	void Start () {
		i=transform.GetChild(0).gameObject.GetComponent<Image>();
		t=GetComponent<RectTransform>();
		maxWidth=t.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetHealth(float health){
		float newWidth=health*maxWidth;
		float oldX=t.rect.x;
		t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,0.0f,newWidth);
		i.color=Color.Lerp(color2, color1, health);
		//t.rect=new Rect(t.rect.left,t.rect.top,newWidth,t.rect.height);
	}
}
