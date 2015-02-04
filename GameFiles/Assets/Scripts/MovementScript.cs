using UnityEngine;
using UnityEditor;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform bullet,explosion;
    public bool isDead;
    public float playerSpeed;
    float bulletTicks,bulletTickLimit=12;
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

    public virtual void shootIfRequired(){
        if(Input.GetMouseButton(0)){
            foreach(BulletSpawnerScript b in GetComponentsInChildren<BulletSpawnerScript>()){
                b.shoot();
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
        //print(InputWrapper.GetAxis("Horizontal1"));
        Vector3 movementVector=new Vector3(InputWrapper.GetAxis("Horizontal1"),0,InputWrapper.GetAxis("Vertical1"))*playerSpeed*Time.deltaTime;
        transform.position+=movementVector;
    }
    public void getHurt(int toLose){
        if(!isDead){
            health-=toLose;
            print(health);
            scoreHandler.claimCombo();
            scoreHandler.die();
        }
    }
}
