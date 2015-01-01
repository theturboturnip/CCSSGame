using UnityEngine;
using System.Collections;

//using System;

public class EnemyHandlerScript : MonoBehaviour {
	MovementScript playerMover;
	public GameObject[] Enemies={};
	public float[] EnemyLengths={};
	public float interval=0f,currentTime=0f,places=100f;
	Vector3 spawnPos;
	// Use this for initialization
	void Start () {
		playerMover=GameObject.Find("Player").GetComponent<MovementScript>();
		reset();
	}
	
	// Update is called once per frame
	void Update () {
		if(places>0){
			currentTime=interval;
			spawnRandomEnemy();
		}else currentTime-=Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.R)) reset();

	}
	void spawnRandomEnemy(){
		int enemyId=Random.Range(0,Enemies.Length);
		GameObject toSpawn=Enemies[enemyId];
		spawnPos+=Vector3.right*EnemyLengths[enemyId];//Vector3.MoveTowards(playerMover.CameraLimit,playerMover.bossLocation,7f);
		Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero));
		places--;
	}
	void reset(){
		places=100;
	}
}
