using UnityEngine;
using System.Collections;
[System.Serializable]
public class Upgrade{
	public string type="",name="";
	public bool enabled=false;
	public float cost=0;
	public Upgrade(string newType,string newName,float newCost){
		type=newType;
		name=newName;
		cost=newCost;
	}
}
