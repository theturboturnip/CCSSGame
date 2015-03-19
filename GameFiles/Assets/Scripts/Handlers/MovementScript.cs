using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform explosion;
    public bool isDead;
    public float playerSpeed;
    public int health=10;
    GameObject pHolder;
    ScoreHandlerScript scoreHandler;
    public Transform[] powerups;

    void Start () {
        SaveLoad.Load();
        //GameObject.Find("Player/BulletSpawners/BulletSpawnRight").SetActive(Game.current.upgrades[0].enabled);
        //GameObject.Find("Player/BulletSpawners/BulletSpawnLeft").SetActive(Game.current.upgrades[0].enabled);
        scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
        pHolder=GameObject.Find("Player/PowerupSlot");
    }

	void Update () {
        if(Time.timeScale>0.0f){
	       moveIfRequired();
            shootIfRequired();
            rigidbody.velocity=Vector3.zero;
            if (isDead){
                isDead=false;
                health=10;
            }
        }
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
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
        Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal")-0.1f,0,Input.GetAxis("Vertical"))*playerSpeed*Time.deltaTime;
        transform.position+=movementVector;
    }
    public void getHurt(int toLose){
        if(!isDead){
            health-=toLose;
            scoreHandler.claimCombo();
            scoreHandler.die();
        }
    }
    public void givePowerup(int id){
        try{
            Destroy(pHolder.transform.GetChild(0).gameObject);
        }catch{}
        Transform p=Instantiate(powerups[id],transform.position,transform.rotation) as Transform;
        p.parent=pHolder.transform;
        print(id);
    }

}
