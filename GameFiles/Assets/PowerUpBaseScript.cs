using UnityEngine;
using System.Collections;

public class PowerUpBaseScript : MonoBehaviour {
	public int id=0;
	// Use this for initialization
	void OnCollisionEnter(Collision c){
		print("Pup col;iede");
		if (c.gameObject.tag.Equals("Player")){
			c.gameObject.GetComponent<MovementScript>().givePowerup(id);
			Destroy(gameObject);
		}
	}
}
