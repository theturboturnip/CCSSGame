using UnityEngine;
using System.Collections;

public class SparkScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
 		StartCoroutine(MyMethod());
	}
	
	// Update is called once per frame
	IEnumerator MyMethod() {
  		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
