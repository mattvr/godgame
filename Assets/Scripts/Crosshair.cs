using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	public GameObject CrosshairSprite;
	private Vector3 _startPos;
	private GameObject LastLookingAt;

	// Use this for initialization
	void Start () {
		_startPos = CrosshairSprite.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		var hit = new RaycastHit();
		if (Physics.Raycast(this.transform.position, transform.forward, out hit, 10f)) {
			CrosshairSprite.transform.position = hit.point;

			var go = hit.collider.gameObject;

			// Stop looking at previous object
			if (go != LastLookingAt && LastLookingAt != null) {
				LastLookingAt.SendMessage("StopLookingAt", SendMessageOptions.DontRequireReceiver);
			}
			// Look at current object
			go.SendMessage("LookingAt", gameObject, SendMessageOptions.DontRequireReceiver);
			LastLookingAt = go;
		}
		// Not looking at anything
		else if (LastLookingAt != null) {
			LastLookingAt.SendMessage("StopLookingAt", SendMessageOptions.DontRequireReceiver);
			LastLookingAt = null;
		}
	}

}
