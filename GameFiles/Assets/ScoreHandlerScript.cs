using UnityEngine;
using System.Collections;

public class ScoreHandlerScript : MonoBehaviour {
    public float totalscore=0,additivescore=0,multiplier=0;
	// Use this for initialization
	void Start () {
	     
	}
	
	// Update is called once per frame
	void Update () {
         //     
	}
    public void EnemyDestroyed(int value){
         if(multiplier==0) multiplier=1;
         else multiplier+=0.1f;
         additivescore+=value;              
    }
}
