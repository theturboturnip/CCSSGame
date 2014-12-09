using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Camera FirstPerson,TopDown;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
         if(Input.GetKeyDown(KeyCode.P)){
             TopDown.enabled=!TopDown.enabled;
             FirstPerson.enabled=!FirstPerson.enabled;
         }       
         if(TopDown.enabled){                 
             Vector3 topdownrot=TopDown.transform.rotation.eulerAngles;
             topdownrot.y=0;
             topdownrot.z=0;
             TopDown.transform.rotation=Quaternion.Euler(topdownrot);
             //TopDown.gameObject.transform.position=new Vector3(0,10,0);
         }
	}
}
