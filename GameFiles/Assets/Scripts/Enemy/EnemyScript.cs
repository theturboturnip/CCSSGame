using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {
    public Transform explosion;
    bool isDead=false;
    ScoreHandlerScript scoreHandler;
    public int health=1;
    public Texture2D hover_img;
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
	    float multiplier=0.1f;
	    int worth=1;
	    //if(causeOfDeath=="Explosion") multiplier=0.2f;
	    
	    scoreHandler.EnemyDestroyed(worth,multiplier);
        GameObject.Destroy(gameObject);
        isDead=true;
	}
	void OnMouseEnter(){
        Cursor.SetCursor(hover_img,new Vector2(16,16),CursorMode.Auto);
    }
    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

