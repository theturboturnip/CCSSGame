using UnityEngine;
using System.Collections;

public class LightThrobScript : MonoBehaviour {

	public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public float duration = 1.0F;
   	Light pointLight;
    void Start(){
    	pointLight=transform.GetChild(0).gameObject.GetComponent<Light>();
    }
    void Update() {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        pointLight.color= Color.Lerp(colorStart, colorEnd, lerp);
        pointLight.intensity=pointLight.color.g*2;
    }

}