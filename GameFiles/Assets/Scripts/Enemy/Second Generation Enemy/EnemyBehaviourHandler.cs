using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviourHandler : MonoBehaviour {
	public string[] queue;// =new string[][]{new string[]{"Attack","5"},new string[]{"Wait","5"}};
	public float[] queueTimes;
	Dictionary<string,EnemyScriptBase> scripts=new Dictionary<string,EnemyScriptBase>();
	float currentTime;
	public string currentBehaviour;
	int currentIndex=0;
	// Use this for initialization
	void Start () {
		//rigidbody.velocity=Vector3.left*5f;
		GetScripts();
		SetScripts();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime-=Time.deltaTime;
		if(currentTime<=0f)
			SetScripts();
	}

	void GetScripts(){
		AddScript<EnemyAttackScript>("Attack");
		AddScript<EnemyWaitScript>("Wait");
	}

	void SetScripts(){
		foreach(KeyValuePair<string, EnemyScriptBase> entry in scripts) {
			entry.Value.Reset();
			entry.Value.enabled=false;
		}
		currentIndex=bind(currentIndex,0,queue.Length);
		currentBehaviour=queue[currentIndex];
		currentTime=queueTimes[currentIndex];
		scripts[currentBehaviour].enabled=true;
		currentIndex++;
		currentIndex=bind(currentIndex,0,queue.Length);
		if(queue.Length==currentIndex) currentIndex=0;
	}
	int bind(int tobind,int lower,int upper){
		//if tobind=1,lower=2, and upper=5
		while(tobind<lower)
			tobind=upper+(tobind-lower);
		while(tobind>upper)
			tobind=lower+(tobind-upper);
		return tobind;
	}
	void AddScript<T>(string name) where T: EnemyScriptBase{
		T script=gameObject.GetComponent<T>();
		//print(script);
		if(script!=null){
			script.enabled=false;
			scripts.Add(name,script);
		}
	}
}
