using UnityEngine;
using System.Collections;

//using System;

public class EnemyHandlerScript : MonoBehaviour {
	public GameObject[] Enemies={};
	public float[] EnemyLengths={};
	public float interval=0f,currentTime=0f,places=1f;
	Vector3 spawnPos;
	// Use this for initialization
	void Start () {
		while(places>0){
			spawnRandomEnemy();
			places--;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) reset();
	}
	void spawnRandomEnemy(){
		int enemyId=0;//Random.Range(0,Enemies.Length);
		GameObject toSpawn=Enemies[enemyId];
		spawnPos+=Vector3.right*EnemyLengths[enemyId];
		Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero));
		places--;
	}
	void reset(){
		places=100;
	}
}
