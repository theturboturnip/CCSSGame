﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerScript : MonoBehaviour {
	ScoreHandlerScript scoreHandler;
	Text additive,total;
	public Material reticule;
	// Use this for initialization
	void Start () {
		GameObject ScoreHandlerObject=GameObject.Find("Handlers/ScoreHandler"),
				   TotalScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/Total"),	
				   AdditiveScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/Additive");
		scoreHandler=ScoreHandlerObject.GetComponent<ScoreHandlerScript>();
		additive=AdditiveScoreObject.GetComponent<Text>();
		total=TotalScoreObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string additiveString=setLength(scoreHandler.additiveScore.ToString("000000"),6)+"*"+setLength(scoreHandler.multiplier.ToString("00.0"),4);
		additive.text=additiveString;
		string totalScoreString=setLength(scoreHandler.totalScore.ToString("0000000000"),11);
		total.text=totalScoreString;
	}
	string setLength(string toMod,int bits){
		if (toMod.Length>=bits){
			return toMod.Substring(toMod.Length-bits);
		}
		while (toMod.Length<bits){
			toMod="0"+toMod;
		}
		return toMod;
	}
}
