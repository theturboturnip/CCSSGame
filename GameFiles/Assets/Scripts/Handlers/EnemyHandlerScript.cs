using UnityEngine;
using System.Collections;

//using System;

public class EnemyHandlerScript : MonoBehaviour {
	public GameObject[] Enemies={};
	public float[] EnemyLengths={};
	public float interval=0f,currentTime=0f,places=1f;
	Vector3[] spawnPositions;
	ScreenHandlerScript screenHandler;
	//Vector3 spawnPos;
	// Use this for initialization
	void Start () {
		screenHandler=GameObject.Find("Handlers/ScreenHandler").GetComponent<ScreenHandlerScript>();
		/*spawnPositions=new Vector3[]{new Vector3(screenHandler.leftX,0f,0f),
						//new Vector3(screenHandler.rightX,0f,0f),
						new Vector3(0f,0f,screenHandler.topZ),
						new Vector3(0f,0f,screenHandler.bottomZ)};*/
		spawnRandomEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) reset();
		//if(places>0){ spawnRandomEnemy(); places--; }
	}
	void spawnRandomEnemy(){
		Vector3 spawnPos=new Vector3(screenHandler.rightX,0f,0f);//spawnPositions[Random.Range(0,spawnPositions.Length)];
		int enemyId=0;//Random.Range(0,Enemies.Length);
		GameObject toSpawn=Enemies[enemyId];
		//spawnPos+=Vector3.right*EnemyLengths[enemyId];
		Transform Enemy=Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero)) as Transform;
		Enemy.LookAt(-spawnPos);
		Enemy.rigidbody.velocity=Vector3.zero;
		Enemy.rigidbody.velocity=(-spawnPos/spawnPos.x);
		print(Enemy.rigidbody.velocity);
		places--;
	}
	void reset(){
		places=100;
	}
}
