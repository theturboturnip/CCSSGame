using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game{
	public float score=0;
	public int deaths=0;
	public static Game current;
	public Game(){
		score=0.0f;
		deaths=0;
	}
}
