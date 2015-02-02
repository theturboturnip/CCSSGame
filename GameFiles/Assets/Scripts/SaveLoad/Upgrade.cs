using UnityEngine;
using System.Collections;
[System.Serializable]
public class Upgrade{
	public string type="",name="";
	public bool enabled=false;
	public Upgrade(string newType,string newName){
		type=newType;
		name=newName;
	}
}
