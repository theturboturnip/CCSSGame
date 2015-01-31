using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerScript : MonoBehaviour {
	ScoreHandlerScript scoreHandler;
	Text additive,total,health;
	public Material reticule;
	MovementScript Player;
	string infinity="∞";
	// Use this for initialization
	void Start () {
		GameObject ScoreHandlerObject=GameObject.Find("Handlers/ScoreHandler"),
				   TotalScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/Total"),	
				   AdditiveScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/Additive"),
				   HealthObject=GameObject.Find("Handlers/UIHandler/Canvas/Health");
		Player=GameObject.Find("Player").GetComponent<MovementScript>();
		scoreHandler=ScoreHandlerObject.GetComponent<ScoreHandlerScript>();
		additive=AdditiveScoreObject.GetComponent<Text>();
		total=TotalScoreObject.GetComponent<Text>();
		health=HealthObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string additiveString=setLength(scoreHandler.additiveScore.ToString("000000"),6)+"*"+setLength(scoreHandler.multiplier.ToString("00.0"),4);
		additive.text=additiveString;
		string totalScoreString=setLength(scoreHandler.totalScore.ToString("0000000000"),11);
		total.text=totalScoreString;
		if(Player.health<0)
			health.text=infinity;
		else
			health.text=Player.health.ToString("000");
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
