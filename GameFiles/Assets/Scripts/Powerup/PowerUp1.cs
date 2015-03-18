using UnityEngine;
using System.Collections;

public class PowerUp1 : MonoBehaviour {
	Transform e;
	BulletSpawnerScript b;
	float rotateSpeed=2.5f;

	// Use this for initialization
	void Start () {
		b=GetComponent<BulletSpawnerScript>();
		b.bulletTickLimit=30.0f;
	}
	
	// Update is called once per frame
	void Update () {
		e=GetNearestWithTag("Enemy");
		//transform.LookAt(e.position);	
		Vector3 pos_=transform.position;
		pos_.y=1;
		transform.position=pos_;
		TryShoot();
	}
	void TryShoot(){
		Vector3 targetDir = e.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
        Vector3 bulletRot=transform.rotation.eulerAngles;
        Vector3 lookAt = Quaternion.LookRotation(targetDir).eulerAngles;
        if(lookAt.y-2.0f<=bulletRot.y&&lookAt.y+2.0f>=bulletRot.y){
			b.shoot(Random.Range(5.0f,-5.0f));
		}	
	}
	Transform GetNearestWithTag(string tag){
		float maxDist=Mathf.Infinity;
		GameObject[] tagged=GameObject.FindGameObjectsWithTag(tag);
		Transform nearest=transform;
		foreach(GameObject g in tagged){
			Vector3 targetPos=g.transform.position;
			float dist=Vector3.Distance(targetPos,transform.position);
			if (dist<maxDist){
				nearest=g.transform;
				maxDist=dist;
			}
		}
		return nearest;
	}
}
