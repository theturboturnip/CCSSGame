using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game{
	public float score=0,money=0,xp=0;
	public string name="";
	public Upgrade[] upgrades={new Upgrade("Weapon","Triple",10000f)};
	public static Upgrade[] newGameUpgrades;
	//public int deaths=0;

	public static Game current;

	public Game(){
		newGameUpgrades=upgrades;
		//score=0.0f;
	}
	public static Game newGame(){
		Game toReturn=new Game();
		toReturn.score=0;
		toReturn.money=0;
		toReturn.xp=0;
		toReturn.name="";
		toReturn.upgrades=newGameUpgrades;
		return toReturn;
	}
}
