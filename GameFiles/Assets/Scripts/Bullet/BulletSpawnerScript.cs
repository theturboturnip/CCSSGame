using UnityEngine;
using System.Collections;

public class BulletSpawnerScript : MonoBehaviour {
	//public string input="Fire1";
	public Transform bullet;
    public float bulletTicks,bulletTickLimit=12;

	GameObject parent;
	// Use this for initialization
	void Start () {
		try{
			parent=transform.parent.gameObject;
		}catch{
			parent=gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		bulletTicks++;
	}
	public void shoot(float offset){
		if(bulletTicks>=bulletTickLimit&&Time.timeScale>0.0f){
			Vector3 r=transform.rotation.eulerAngles;
			r.y+=offset;
			Quaternion rot=Quaternion.Euler(r);
        	Transform Bullet=Instantiate(bullet,transform.position,rot) as Transform;
        	try{
            	Bullet.gameObject.GetComponent<BulletScript>().Shooter=parent;
        	}catch{}
            bulletTicks=0;
        }
	}
}
