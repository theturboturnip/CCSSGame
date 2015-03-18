using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {
    public Transform explosion,powerup;
    bool isDead=false;
    ScoreHandlerScript scoreHandler;
    public int health=1,p_id=0;
    public Texture2D hover_img;
    public float dropChance=2.0f;
	void Start () {
		scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
	}
	void Update(){
		transform.position=new Vector3(transform.position.x,0f,transform.position.z);
		if(Camera.main.WorldToScreenPoint(transform.position).x<0) GameObject.Destroy(gameObject);
	}
	
	public void OnCollisonStay (Collision c){
		if(c.gameObject.tag!="Enemy"){
			transform.Translate(Vector3.back);
		}
	}
	public void getHurt(int value,GameObject go){
		try{
			if(!isDead) health-=value;
			if(health<=0) die("Bullet"); 
		}catch{}
	}
	void die(string causeOfDeath){
		Instantiate(explosion,transform.position,transform.rotation);
		float r=Random.Range(0.0f,10.0f);
		if(r<=dropChance){
			Transform g=Instantiate(powerup,transform.position,Quaternion.identity) as Transform;
			PowerUpItemScript p=g.gameObject.GetComponent<PowerUpItemScript>();
			p.id=p_id;
			print(p.id);
		}
	    float multiplier=0.1f;
	    int worth=1;	    
	    scoreHandler.EnemyDestroyed(worth,multiplier);
        Destroy(gameObject);
        isDead=true;
	}
}

