using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopScript : MonoBehaviour {
	public GameObject button;
	Text scoreText;
	// Use this for initialization
	void Start () {
		SaveLoad.Load();
		scoreText=GameObject.Find("Canvas/MainPanel/UpgradeText").GetComponent<Text>();
		Transform parent=GameObject.Find("Canvas/MainPanel/ButtonPanel").transform;
		foreach(Upgrade u in Game.current.upgrades){
			GameObject b=Instantiate(button,Vector3.zero,Quaternion.Euler(Vector3.zero)) as GameObject;
			b.transform.parent=parent;
			b.name=u.type+u.name;
			b.transform.GetChild(0).gameObject.GetComponent<Text>().text=u.type+u.name+":"+u.cost;
			Button b_=b.GetComponent<Button>();
			b_.onClick.AddListener(() => {
  	 			//handle click here
  	 			BuyUpgrade(u.name,b_);
 			});
 			b_.interactable=!u.enabled;
		}
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text="Upgrade Shop\nTotal Score: "+Game.current.score;
	}
	public void BuyUpgrade(string name,Button b){
		Upgrade toBuy=Game.current.upgrades[0];
		foreach (Upgrade u in Game.current.upgrades){
			if(u.name==name)
				toBuy=u;
		}
		bool canBuy=Game.current.score>=toBuy.cost;
		if(canBuy){
			Game.current.score-=toBuy.cost;
			b.interactable=false;
			toBuy.enabled=true;
			print(toBuy.cost);
			SaveLoad.Save();
		}
	}
	public void StartGame(){
		#if UNITY_IPHONE
			Application.LoadLevel(3);
			return;
		#endif
		#if UNITY_ANDROID
			Application.LoadLevel(3);
			return;
		#endif
		Application.LoadLevel(2);
	}
}
