using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public string name;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(Game.current.weapons[Game.current.weaponIndex]==name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
