using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BorgHealth : MonoBehaviour {
	public Color color1,color2;
	RectTransform t;
	Image i;
	float maxWidth,targetWidth;
	// Use this for initialization
	void Start () {
		i=transform.GetChild(0).gameObject.GetComponent<Image>();
		t=GetComponent<RectTransform>();
		maxWidth=t.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
		if(t.rect.width!=targetWidth){
			if(t.rect.width>targetWidth){
				float newWidth=t.rect.width-1f;
				t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,0.0f,newWidth);
			}else
				t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,0.0f,targetWidth);
		}

	}
	public void SetHealth(float health){
		targetWidth=health*maxWidth;
		i.color=Color.Lerp(color2, color1, health);
		//t.rect=new Rect(t.rect.left,t.rect.top,newWidth,t.rect.height);
	}
}
