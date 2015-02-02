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
		if(Input.GetMouseButton(0)&&bulletTicks>=bulletTickLimit){
        	Transform Bullet=Instantiate(bullet,transform.position/*+transform.TransformDirection(Vector3.forward)*/,transform.rotation) as Transform;
            Bullet.gameObject.GetComponent<BulletScript>().Shooter=parent;
            bulletTicks=0;
            parent.GetComponent<AudioSource>().Play();
        }
        bulletTicks++;
	}
}
