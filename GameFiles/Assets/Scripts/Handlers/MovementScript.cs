using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform bullet,explosion;
    public bool isDead;
    public float playerSpeed;
    public int health=10;
    ScoreHandlerScript scoreHandler;

    void Start () {
        SaveLoad.Load();
        if(!Game.current.upgrades[0].enabled){
            GameObject.Find("Player/BulletSpawnRight").SetActive(false);
            GameObject.Find("Player/BulletSpawnLeft").SetActive(false);
        }
        scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
    }

	// Update is called once per frame
	void Update () {
	    moveIfRequired();
        shootIfRequired();
        rigidbody.velocity=Vector3.zero;
        if (isDead){
            isDead=false;
            health=10;
        }
    }  

    public void shootIfRequired(){
        if(Input.GetMouseButton(0)){
            foreach(BulletSpawnerScript b in GetComponentsInChildren<BulletSpawnerScript>()){
                b.shoot(Random.Range(Game.current.bullet_spread,-Game.current.bullet_spread));
            }
        }
    }

    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }

    public virtual void rotateIfRequired(){
        Vector3 mouseWorldPos=Input.mousePosition-new Vector3(16,16,0);
        transform.LookAt(ScreenToWorldPoint(mouseWorldPos));
    }
    void moveIfRequired(){
        rotateIfRequired();
        Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*playerSpeed*Time.deltaTime;
        transform.position+=movementVector;
    }
    public void getHurt(int toLose){
        if(!isDead){
            health-=toLose;
            scoreHandler.claimCombo();
            scoreHandler.die();
        }
    }
}
