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
		rigidbody.velocity=Vector3.left*5f;
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
		if(queue.Length==currentIndex){
			currentIndex=0;
		}
		print(currentIndex+","+queue.Length);
		currentBehaviour=queue[currentIndex];
		currentTime=queueTimes[currentIndex];
		currentIndex++;
		scripts[currentBehaviour].enabled=true;
		scripts[queue[currentIndex-1]].enabled=false;
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
