using UnityEngine;
using System.Collections;

public class MoverScript : MonoBehaviour {
    public bool CameraRelative=false;
	public float  MoveSpeed=5f;
	public Transform Bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!CameraRelative) {
			Vector3 newRot=transform.rotation.eulerAngles;
			newRot.y+=Input.GetAxis("Horizontal");
			transform.rotation=Quaternion.Euler(newRot);
			transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * MoveSpeed);
			Vector3 bulletSpawn=transform.position+transform.forward;
			bulletSpawn.y-=1;
			if(Input.GetButtonDown("Fire1")) 
				Instantiate(Bullet,bulletSpawn,transform.rotation);
		}else{
			Vector3 posMod=new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
			transform.position+=posMod*Time.deltaTime*MoveSpeed;
			Vector3 inputPosition = Input.mousePosition; 
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width - inputPosition.x, Screen.height - inputPosition.y, Camera.main.transform.position.z - 2f));
			RaycastHit hit;
			Physics.Raycast(ray,out hit);
			Vector3 tolookat=hit.point;
			tolookat.y=transform.position.y;
			transform.LookAt(tolookat);
		}
	}
	Vector3 noY(Vector3 toMod){
		Vector3 toReturn = toMod;
		toReturn.y = 0;
		return toReturn;
	}
}
