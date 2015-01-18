using UnityEngine;
using System.Collections;

public class ScreenHandlerScript : MonoBehaviour {
	GameObject Player;
	public float leftX,rightX,topZ,bottomZ;

	void Start () {
		Player=GameObject.Find("Player");
		Vector3 top=new Vector3(Screen.width/2,0f,0f),
				bottom=new Vector3(Screen.width/2,Screen.height,0f),
				left=new Vector3(0f,Screen.height/2,0f),
				right=new Vector3(Screen.width,Screen.height/2,0f);
		Vector3 screenTop=ScreenToWorldPoint(top),
				screenBottom=ScreenToWorldPoint(bottom),
				screenLeft=ScreenToWorldPoint(left),
				screenRight=ScreenToWorldPoint(right);
		leftX=screenLeft.x;
		rightX=screenRight.x;
		topZ=screenTop.z;
		bottomZ=screenBottom.z;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] allObjects=new GameObject[]{Player};
		rightX=-rightX;
		leftX=-leftX;
		bottomZ=-bottomZ;
		topZ=-topZ;
		foreach(GameObject obj in allObjects){
			Transform trans=obj.transform;
			if(trans.root==trans){
				Vector3 screenPos=WorldToScreenPoint(trans.position);    
        		Vector3 toMove=trans.position;
        		if(screenPos.x<0)
            		toMove.x=rightX;
        		else if(screenPos.x>Screen.width)
            		toMove.x=leftX;
        		if(screenPos.y<0)
            		toMove.z=bottomZ;
        		else if(screenPos.y>Screen.height)
            		toMove.z=topZ;
        		trans.position=toMove;
        	}
        }
        rightX=-rightX;
		leftX=-leftX;
		bottomZ=-bottomZ;
		topZ=-topZ;
   	}

	Vector3 ScreenToWorldPoint(Vector3 pos){
         pos.z=Camera.main.transform.position.y;
         //print(pos.z);
         return Camera.main.ScreenToWorldPoint(pos);
    }

    Vector3 WorldToScreenPoint(Vector3 pos){
         return Camera.main.WorldToScreenPoint(pos);
    }
}
