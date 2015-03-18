using UnityEngine;
using System.Collections;

public class PowerUpItemScript : MonoBehaviour {
	public int id=0;
	float moveSpeed;
	Transform p;
	// Use this for initialization
	void Start(){
		p=GameObject.Find("Player").transform;
	}
	void Update(){
		float moveSpeed_=Time.deltaTime*(10.0f-Vector3.Distance(transform.position,p.position));
		if(moveSpeed_<=0.0f)
			moveSpeed_=Time.deltaTime;
		if(moveSpeed_>moveSpeed)
			moveSpeed=moveSpeed_;
		transform.position=Vector3.MoveTowards(transform.position,p.position,moveSpeed);
		if(p.position==transform.position)
			GivePowerup();

	}
	void GivePowerup(){
		p.gameObject.GetComponent<MovementScript>().givePowerup(id);
		Destroy(gameObject);
	}
}
