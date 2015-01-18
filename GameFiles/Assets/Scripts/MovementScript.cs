using UnityEngine;
using UnityEditor;
using System.Collections;

public class MovementScript : MonoBehaviour {
    public Transform bullet;
    public float playerSpeed;
    float bulletTicks,bulletTickLimit=10;
    public int health=10;
    ScoreHandlerScript scoreHandler;

    void Start () {
        scoreHandler=GameObject.Find("Handlers/ScoreHandler").GetComponent<ScoreHandlerScript>();
    }

	// Update is called once per frame
	void Update () {
	    moveIfRequired();
        shootIfRequired();
        rigidbody.velocity=Vector3.zero;
    }  

    Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=12;
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }

    void shootIfRequired(){
        if(Input.GetMouseButton(0)&&bulletTicks>=bulletTickLimit){
            Transform Bullet=Instantiate(bullet,transform.position+transform.TransformDirection(Vector3.forward),transform.rotation) as Transform;
            Bullet.gameObject.GetComponent<BulletScript>().Shooter=gameObject;
            bulletTicks=0;
            gameObject.GetComponent<AudioSource>().Play();
         }
         bulletTicks++;
    }

    void moveIfRequired(){
        Vector3 mouseWorldPos=Input.mousePosition-new Vector3(16,16,0);
        transform.LookAt(ScreenToWorldPoint(mouseWorldPos));
        Vector3 movementVector=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*playerSpeed*Time.deltaTime;
        transform.position+=movementVector;

        //Checking if the player is offscreen
        
        /*if(xOff)
            if()
            transform.position=new Vector3(transform.position.x-movementVector.x,transform.position.y,transform.position.z);
        if(yOff)
            transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z-movementVector.z);
    */}
    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag!="Enemy"){
            if(c.gameObject.tag=="Bullet"){
                BulletScript bullet = c.gameObject.GetComponent<BulletScript>();
                if((bullet.Shooter==gameObject&&bullet.ticks>=90))
                    return;
            }
            getHurt(1);
        }
    }
    public void getHurt(int toLose){
        health-=toLose;
        scoreHandler.claimCombo();
        if(health==0){ EditorApplication.isPlaying=false; Application.Quit(); }
    }
}
