using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool Self = false;

	// Use this for initialization
	void Start () {
		
	}

	public void Init () {
		if (Self) {
			var renderers = this.GetComponentsInChildren<Renderer>();
			foreach (var r in renderers) {
				r.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
