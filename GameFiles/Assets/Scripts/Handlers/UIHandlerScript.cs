using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerScript : MonoBehaviour {
	ScoreHandlerScript scoreHandler;
	Text additive,total;
	BorgHealth health;
	public Material reticule;
	MovementScript Player;
	string infinity="∞";
	int maxHealth;
	// Use this for initialization
	void Start () {
		GameObject ScoreHandlerObject=GameObject.Find("Handlers/ScoreHandler"),
				   TotalScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/ScorePanel/Total"),	
				   AdditiveScoreObject=GameObject.Find("Handlers/UIHandler/Canvas/ScorePanel/Additive"),
				   HealthObject=GameObject.Find("Handlers/UIHandler/Canvas/HealthPanel/Panel");
		Player=GameObject.Find("Player").GetComponent<MovementScript>();
		scoreHandler=ScoreHandlerObject.GetComponent<ScoreHandlerScript>();
		additive=AdditiveScoreObject.GetComponent<Text>();
		total=TotalScoreObject.GetComponent<Text>();
		health=HealthObject.GetComponent<BorgHealth>();
		maxHealth=Player.health;
	}
	
	// Update is called once per frame
	void Update () {
		string additiveString=setLength(scoreHandler.additiveScore.ToString("00000"),5)+"*"+setLength(scoreHandler.multiplier.ToString("0.0"),3);
		additive.text=additiveString;
		string totalScoreString=setLength(scoreHandler.totalScore.ToString("00000000"),9);
		total.text=totalScoreString;
		health.SetHealth((float)Player.health/(float)maxHealth);
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
