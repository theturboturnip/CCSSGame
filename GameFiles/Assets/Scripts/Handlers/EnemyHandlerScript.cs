using UnityEngine;
using System.Collections;

public class EnemyHandlerScript : MonoBehaviour {
	public GameObject[] Enemies={};
	public float interval=3f,currentTime=0f,places=1f;
	Vector3[] spawnPositions;
	ScreenHandlerScript screenHandler;

	void Start () {
		screenHandler=GameObject.Find("Handlers/ScreenHandler").GetComponent<ScreenHandlerScript>();
	}
	
	void Update () {
		currentTime-=Time.deltaTime;
		if(currentTime<=0){
			spawnRandomEnemy();
			currentTime=interval;
		}
	}
	void spawnRandomEnemy(){
		Vector3 spawnPos=new Vector3(screenHandler.rightX,0f,0f);
		int enemyId=Random.Range(0,Enemies.Length);
		GameObject toSpawn=Enemies[enemyId];
		spawnPos.z=Random.Range(screenHandler.topZ,screenHandler.bottomZ);
		Transform Enemy=Instantiate(toSpawn.transform,spawnPos,Quaternion.Euler(Vector3.zero)) as Transform;
		spawnPos.z=-spawnPos.z;
		Enemy.LookAt(-spawnPos);
		Enemy.rigidbody.velocity=(Enemy.forward*4f);
	}

}
