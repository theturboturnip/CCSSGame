using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game{
	public float score=0,money=0,xp=0;
	public string name="";
	public static int currentIndex;
	//public int deaths=0;

	public static Game current;
	public Game(){
		//score=0.0f;
	}
}
