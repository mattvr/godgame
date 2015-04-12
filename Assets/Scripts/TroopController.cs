using UnityEngine;
using System.Collections;

public class TroopController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Spawn() {
		while (true) {
			yield return new WaitForSeconds(2f); // each loop, wait for 1 second before continuing

			print ("zombie spawner spawn");
			GameObject unit = (GameObject)Resources.Load("Prefabs/Unit/zombie");
			Instantiate (unit, new Vector3 (0F + .1F*(Random.Range (-9,9)),0.1F,2.0f), Quaternion.identity); 
			unit.GetComponent<BoardObject>().direction = Vector3.back;
		}
		// done
	}

}
