using UnityEngine;
using System.Collections;

public class BulletSpawnerScript : MonoBehaviour {
	//public string input="Fire1";
	public Transform bullet;
    float bulletTicks,bulletTickLimit=12;

	GameObject parent;
	// Use this for initialization
	void Start () {
		parent=transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		bulletTicks++;
	}
	public void shoot(){
		if(bulletTicks>=bulletTickLimit){
			print("Shooting");
        	Transform Bullet=Instantiate(bullet,transform.position,transform.rotation) as Transform;
            Bullet.gameObject.GetComponent<BulletScript>().Shooter=parent;
            bulletTicks=0;
        }
	}
}
