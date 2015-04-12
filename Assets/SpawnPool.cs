using UnityEngine;
using System.Collections;

public class SpawnPool : MonoBehaviour {

	public static SpawnPool Instance;
	public GameObject SpawnLocation;

	void Awake() {
		Instance = this;
	}

	public void Spawn(Object obj) {
		var go = Network.Instantiate(obj, SpawnLocation.transform.position, Quaternion.identity, 0);
	}

}
