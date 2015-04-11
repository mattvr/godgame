using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	// Set via inspector
	public GameObject CrosshairSprite;
	public Renderer FillSprite;
	public GameObject Placer;
	public GameObject Hand;

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

			Look (lookable);
		}
		else {
			Look (null);
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			HidePlacer();
		}
	}

	void Look(ILookable lookable) {
		float fill = 0;

		// Looking at nothing
		if (lookable == null) {
			timeLooking = 0;
			if (LastLookingAt != null)
				LastLookingAt.StopLooking(gameObject);
			fireActivated = false;
			LastLookingAt = null;
		}
		// Looking at something
		else {
			timeLooking += Time.deltaTime;

			// Changed what we're looking at
			if (LastLookingAt != lookable) {
				if (LastLookingAt != null)
					LastLookingAt.StopLooking(gameObject);
				fireActivated = false;
				lookable.StartLooking(gameObject);
			}
			
			LastLookingAt = lookable;
			lookable.Looking(gameObject);
			// Fire activation
			if (!fireActivated && timeLooking > lookable.TimeToActivate) {
				Activate(lookable);
				LastLookingAt = null;
			}
			else {
				fill = timeLooking / lookable.TimeToActivate;
			}
		}
		Fill (fill);
	}

	void Fill(float n) {
		FillSprite.material.SetFloat("_DissolveAmount", 1 - n);
	}

	void Activate(ILookable lookable) {
		lookable.Activate(gameObject);
		fireActivated = true;
		ShowPlacer();
	}

	public void ShowPlacer() {
		Placer.SetActive(true);
	}

	public void HidePlacer() {
		Placer.SetActive(false);
	}
}
