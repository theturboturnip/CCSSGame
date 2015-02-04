using UnityEngine;
using UnityEditor;
using System.Collections;

public class ScoreHandlerScript : MonoBehaviour {
    public float totalScore=0,additiveScore=0,multiplier=0,scoreUnit=100;
    float additiveAddTimer=0,additiveAddTime=4,totalScoreAdditive=10,totalScoreGoal=0;
    public MovementScript player;
	// Use this for initialization
	void Start () {
        SaveLoad.Load();
	    player=GameObject.Find("Player").GetComponent<MovementScript>();
        totalScore=Game.current.score;
        totalScoreGoal=Game.current.score;
	}
	
	// Update is called once per frame
	void Update () {
        if(multiplier>99.9f) multiplier=99.9f;
    	if(additiveAddTimer<=0&&additiveScore*multiplier>0){
    		totalScoreGoal+=additiveScore*multiplier;
    		additiveScore=0;
    		multiplier=0;
    		totalScoreAdditive=(totalScoreGoal-totalScore)/25;
    	}
    	else additiveAddTimer-=Time.deltaTime;
    	if(totalScore<totalScoreGoal) totalScore+=totalScoreAdditive;
    	else if(totalScore>totalScoreGoal) totalScore=totalScoreGoal;
        if(Input.GetButton("Quit")||player.health==0){
            Game.current.score=totalScoreGoal+(additiveScore*multiplier);
            SaveLoad.Save();
            Application.LoadLevel(1);
        }
	}
    public void EnemyDestroyed(int value,float toAddToMultiplier){
         if(multiplier==0&&toAddToMultiplier!=0) multiplier=1;
         else multiplier+=toAddToMultiplier;
         additiveScore+=value*scoreUnit; 
         additiveAddTimer=additiveAddTime;             
    }

    public void claimCombo(){
        additiveAddTimer=0;
    }
    public void die(){
    //    Game.current.deaths+=1;
    }
}
