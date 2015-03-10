using UnityEngine;
using System.Collections;

public class EnemyAttackScript : EnemyScriptBase {
	Transform target;
	public float rotateSpeed=2.5f;
    BulletSpawnerScript b;

	// Use this for initialization
	void Start () {
		target=GameObject.Find("Player").transform;
		b=transform.GetChild(0).gameObject.GetComponent<BulletSpawnerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt(target);
		//print("AttackEnabled");
		Vector3 targetDir = target.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
        Vector3 bulletRot=transform.rotation.eulerAngles;
        Vector3 lookAt = Quaternion.LookRotation(targetDir).eulerAngles;
        if(lookAt.y-10<=bulletRot.y&&lookAt.y+10>=bulletRot.y){
			b.shoot(Random.Range(25.0f,-25.0f));
		}	
	}
	public override void Reset(){}
}
