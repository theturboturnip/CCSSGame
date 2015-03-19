using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {
	public Transform target,explosion;
	public float speed=15.0f,lifespan=5.0f;
	public bool smartRotation=true;
	// Use this for initialization
	void Start () {
		Ray r=Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r,out hit)){
			target=hit.transform;
		}else{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		lifespan-=Time.deltaTime;
		print(target);
		if(target!=null){
			float rotateSpeed=1.0f;
			if(smartRotation)
				rotateSpeed=10.0f-Vector3.Distance(target.position,transform.position);
			rotateSpeed=Mathf.Clamp(rotateSpeed,1.0f,Mathf.Infinity);
			transform.rotation=Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,target.position-transform.position,rotateSpeed*Time.deltaTime,0.0f));
			transform.Translate(Vector3.forward*speed*Time.deltaTime);
		}
		if(target==null||lifespan<=0.0f){
			print(lifespan);
			Explode();
		}
	}
	void Explode(){
		transform.GetChild(1).parent=null;
		Instantiate(explosion,transform.position,transform.rotation);
		Destroy(gameObject);
	}
	void OnCollisionEnter(Collision c){
		GameObject go=c.transform.gameObject;
		if(go.tag=="Enemy") go.GetComponent<EnemyScript>().getHurt(100,go);
		else if (go.tag=="Powerup") go.GetComponent<PowerUpBaseScript>().getHit();
		Explode();
	}
}
