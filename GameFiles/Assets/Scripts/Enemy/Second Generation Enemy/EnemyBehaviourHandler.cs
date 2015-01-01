using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviourHandler : MonoBehaviour {
	public string[,] queue=new string[,]{{"Attack","5"},{"Wait","5"}};
	Dictionary<string,EnemyScriptBase> scripts=new Dictionary<string,EnemyScriptBase>();
	float currentTime;
	public string enabled;
	int currentIndex=0;
	// Use this for initialization
	void Start () {
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
		AddScript<EnemyAttackScript>("attack");
		AddScript<EnemyWaitScript>("wait");
	}

	void SetScripts(){
		if(queue.Length==currentIndex){
			currentIndex=0;
		}
		enabled=queue[currentIndex,0];
		currentTime=float.Parse(queue[currentIndex,1]);
		currentIndex++;
	}

	void AddScript<T>(string name) where T: EnemyScriptBase{
		T script=gameObject.GetComponent<T>();
		if(script!=null)
			scripts.Add(name,script);
	}
}
