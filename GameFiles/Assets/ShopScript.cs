using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SaveLoad.Load();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void BuyUpgrade(string name){
		Upgrade toBuy=Game.current.upgrades[0];
		foreach (Upgrade u in Game.current.upgrades){
			if(u.name==name)
				toBuy=u;
		}
		toBuy.enabled=!toBuy.enabled;
		print(toBuy.enabled);
		SaveLoad.Save();
	}
}
