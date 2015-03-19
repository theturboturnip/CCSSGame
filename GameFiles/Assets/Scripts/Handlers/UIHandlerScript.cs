using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerScript : MonoBehaviour {
	ScoreHandlerScript scoreHandler;
	Text additive,total;
	BorgHealth health;
	MovementScript Player;
	int maxHealth;
	float currentCursorIndex=0.0f;
	public Texture2D[] OnEnemy=new Texture2D[3],OnPlayer=new Texture2D[3],Idle=new Texture2D[3];
	Texture2D[] CurrentCursor;
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
		CurrentCursor=Idle;
		//TellPlayer("This is a confirmation.",true);
	}
	void CursorTick(){
		currentCursorIndex+=Time.deltaTime*4;
		
		Ray r=Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r,out hit)){
			GameObject g=hit.transform.gameObject;
			CurrentCursor=Idle;
			if(g.tag=="Player"){
				CurrentCursor=OnPlayer;
				currentCursorIndex=0.0f;
			}else if (g.tag=="Enemy"){
				CurrentCursor=OnEnemy;
				currentCursorIndex=0.0f;
			}
		}
		if(currentCursorIndex>CurrentCursor.Length){
			currentCursorIndex=0.0f;
		}
		Cursor.SetCursor(CurrentCursor[(int)currentCursorIndex],new Vector2(16,16),CursorMode.Auto);

	}
	// Update is called once per frame
	void Update () {
		string additiveString=setLength(scoreHandler.additiveScore.ToString("00000"),5)+"*"+setLength(scoreHandler.multiplier.ToString("0.0"),3);
		additive.text=additiveString;
		string totalScoreString=setLength(scoreHandler.totalScore.ToString("00000000"),9);
		total.text=totalScoreString;
		health.SetHealth((float)Player.health/(float)maxHealth);
		//if(Input.GetMouseButtonDown(0))
		CursorTick();
		if(Input.GetKeyDown(KeyCode.P)){
			Time.timeScale=1-Time.timeScale;	
		}
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
