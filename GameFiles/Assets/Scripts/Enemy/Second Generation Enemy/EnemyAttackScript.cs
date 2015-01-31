using UnityEngine;
using UnityEditor;
using System.Collections;

public class EnemyAttackScript : EnemyScriptBase {
	Transform target;
	public Transform bullet;
	public float rotateSpeed=2.5f;
    float bulletTicks,bulletTickLimit=30;

	// Use this for initialization
	void Start () {
		target=GameObject.Find("Player").transform;
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
		bulletRot.y+=Random.Range(-5.0f,5.0f);
        Vector3 lookAt = Quaternion.LookRotation(targetDir).eulerAngles;
        if(lookAt.y-10<=bulletRot.y&&lookAt.y+10>=bulletRot.y){
			 if(bulletTicks>=bulletTickLimit){	
				Transform Bullet=Instantiate(bullet,transform.position+transform.TransformDirection(Vector3.forward),Quaternion.Euler(bulletRot)) as Transform;
				Bullet.gameObject.GetComponent<BulletScript>().Shooter=gameObject;
        		bulletTicks=0;
        	//Time.timeScale=0f;
    		}
    	}	
      	bulletTicks++;
	}
	public override void Reset(){
		bulletTicks=0;
	}
}
