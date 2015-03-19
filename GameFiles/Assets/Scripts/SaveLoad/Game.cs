using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game{
	public float score=0,money=0,xp=0,bullet_spread=10.0f;
	public string name="";
	public string[] weapons={"SingleShot","Shotgun","Missile"};
	public int weaponIndex=0;
	public Upgrade[] upgrades={new Upgrade("Weapon","Triple",10000f)};
	public static Upgrade[] newGameUpgrades;
	public static string[] newGameWeapons;
	//public int deaths=0;

	public static Game current;

	public Game(){
		newGameUpgrades=upgrades;
		newGameWeapons=weapons;
		//score=0.0f;
	}
	public static Game newGame(){
		Game toReturn=new Game();
		toReturn.score=0;
		toReturn.money=0;
		toReturn.xp=0;
		toReturn.bullet_spread=10.0f;
		toReturn.name="";
		toReturn.upgrades=newGameUpgrades;
		toReturn.weapons=newGameWeapons;
		toReturn.weaponIndex=0;
		return toReturn;
	}
}
