using UnityEngine;
using System.Collections;

//using System;

public class EnemyHandlerScript : MonoBehaviour {
	public GameObject[] Enemies={};
	public float[] EnemyLengths={};
	public float interval=3f,currentTime=0f,places=1f;
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
	}
	
	// Update is called once per frame
	void Update () {
		currentTime-=Time.deltaTime;
		if(currentTime<=0){
			spawnRandomEnemy();
			currentTime=interval;
		}
		//if(places>0){ spawnRandomEnemy(); places--; }
	}
	void spawnRandomEnemy(){
		Vector3 spawnPos=new Vector3(screenHandler.rightX,0f,0f);//spawnPositions[Random.Range(0,spawnPositions.Length)];
		int enemyId=0;//Random.Range(0,Enemies.Length);
		GameObject toSpawn=Enemies[enemyId];
		//spawnPos+=Vector3.right*EnemyLengths[enemyId];
		spawnPos.z=Random.Range(screenHandler.topZ,screenHandler.bottomZ);
		Transform Enemy=Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero)) as Transform;
		spawnPos.z=0f;
		Enemy.LookAt(-spawnPos);
		Enemy.rigidbody.velocity=Vector3.zero;
		Enemy.rigidbody.velocity=((-spawnPos/spawnPos.x)*4f);
	}

}
