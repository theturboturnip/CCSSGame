using UnityEngine;
using System.Collections;

//using System;

public class EnemyHandlerScript : MonoBehaviour {
	MovementScript playerMover;
	public GameObject[] Enemies={};
	public float interval=1f,currentTime=0f;
	// Use this for initialization
	void Start () {
		playerMover=GameObject.Find("Player").GetComponent<MovementScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime<=0f){
			currentTime=interval;
			spawnRandomEnemy();
		}else currentTime-=Time.deltaTime;
	}
	void spawnRandomEnemy(){
		GameObject toSpawn=Enemies[Random.Range(0,Enemies.Length)];
		Vector3 spawnPos=playerMover.CameraLimit;//Vector3.MoveTowards(playerMover.CameraLimit,playerMover.bossLocation,7f);
		Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero));
	}
}
