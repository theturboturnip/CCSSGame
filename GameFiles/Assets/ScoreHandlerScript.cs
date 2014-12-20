using UnityEngine;
using System.Collections;

public class ScoreHandlerScript : MonoBehaviour {
    public float totalScore=0,additiveScore=0,multiplier=0,totalScoreGoal=0;
    float additiveAddTimer=0,additiveAddTime=4;
	// Use this for initialization
	void Start () {
	     
	}
	
	// Update is called once per frame
	void Update () {
    	if(additiveAddTimer<=0){
    		totalScoreGoal+=additiveScore*multiplier;
    		additiveScore=0;
    		multiplier=0;
    	}
    	else additiveAddTimer-=Time.deltaTime;
    	if(totalScore<totalScoreGoal) totalScore+=5;         
	}
    public void EnemyDestroyed(int value){
         if(multiplier==0) multiplier=1;
         else multiplier+=0.1f;
         additiveScore+=value; 
         additiveAddTimer=additiveAddTime;             
    }
}
