using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BorgFont : MonoBehaviour {
	public Color color1,color2;
	Text t;
	public Font f;
	// Use this for initialization
	void Start () {
		t=GetComponent<Text>();
		t.font=f;
	}
	
	// Update is called once per frame
	void Update () {
 		float lerp = Mathf.PingPong(Time.time, 1.0F);
        t.color = Color.Lerp(color1, color2, lerp);
  	}
}
