using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	public GameObject CrosshairSprite;
	public Renderer FillSprite;
	private Vector3 _startPos;
	private ILookable LastLookingAt;
	private float timeLooking = 0f;
	private bool fireActivated = false;

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

			var lookable = go.GetComponent<ILookable>();
			if (lookable == null) return;

			// Stop looking at previous object
			if (lookable != LastLookingAt && LastLookingAt != null) {
				LastLookingAt.StopLooking(gameObject);
				timeLooking = 0;
				fireActivated = false;
				lookable.StartLooking(gameObject);
			}
			else if (LastLookingAt == null) {
				timeLooking = 0;
				lookable.StartLooking(gameObject);
			}

			// Activate if enough time has passed
			timeLooking += Time.deltaTime;
			if (!fireActivated && timeLooking > lookable.TimeToActivate) {
				lookable.Activate (gameObject);
				fireActivated = true;
			}

			// Update variables
			Fill (timeLooking / lookable.TimeToActivate);
			LastLookingAt = lookable;
			LastLookingAt.Looking(gameObject);
		}
		// Not looking at anything
		else if (LastLookingAt != null) {
			Fill (0);
			timeLooking = 0;
			LastLookingAt.StopLooking(gameObject);
			fireActivated = false;
			LastLookingAt = null;
		}
	}

	void Fill(float n) {
			FillSprite.material.SetFloat("_DissolveAmount", 1 - n);
	}

}
