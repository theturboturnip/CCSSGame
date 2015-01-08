using UnityEngine;
using UnityEditor;
using System.Collections;

public class EnemyAttackScript : EnemyScriptBase {
	Transform target;
	public Transform bullet;
    float bulletTicks,bulletTickLimit=30;

	// Use this for initialization
	void Start () {
		target=GameObject.Find("Player").transform;
		bullet.gameObject.tag="Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		//print("AttackEnabled");
		if(bulletTicks>=bulletTickLimit){
			Instantiate(bullet,transform.position+transform.TransformDirection(Vector3.forward),transform.rotation);
        	bulletTicks=0;
        	//Time.timeScale=0f;
    	}
      	bulletTicks++;
	}
	public override void Reset(){
		bulletTicks=0;
	}
}
