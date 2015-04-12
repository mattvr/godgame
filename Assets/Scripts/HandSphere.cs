using UnityEngine;
using System.Collections;

public class HandSphere : MonoBehaviour {

	// Set these
	public GameObject GroundIndicator;
	public LineRenderer Line;

	// Use this for initialization
	void Start () {
	
	}

	bool hitGround = false;
	
	// Update is called once per frame
	void Update () {
		var player = KinectStream.Instance.getPlayer();

		if (Input.GetButton("Fire2")) {
			print ("SPAWN ME BABY!");

			GameObject unit = (GameObject)Resources.Load("Prefabs/Unit/archer");

			print (unit);
			Instantiate (unit, new Vector3 (0,0,0.5f), Quaternion.identity); 
			unit.GetComponent<BoardObject>().direction = Vector3.forward;

		}

		if (player != null)
			this.transform.localPosition = player.getJoint(11);

		var hit = new RaycastHit();

		if (Physics.Raycast(transform.position, transform.up * -1, out hit, 10f) && hit.collider.gameObject.CompareTag("Board")) {
			GroundIndicator.SetActive(true);
			Line.gameObject.SetActive(true);
			GroundIndicator.transform.position = hit.point;
			if (!hitGround) {
				GroundIndicator.transform.localScale = new Vector3(0.1f, 0.005f, 0.1f);
				iTween.ScaleTo(GroundIndicator, iTween.Hash("scale", new Vector3(0.5f, 0.005f, 0.5f), "time", 2f));
				StartCoroutine(HitGround());
				hitGround = true;
			}
			Line.SetPosition(0, this.transform.position);
			Line.SetPosition(1, GroundIndicator.transform.position);
		}
		else {
			GroundIndicator.SetActive(false);
			Line.gameObject.SetActive(false);
			StopCoroutine(HitGround());
			hitGround = false;
		}
	}

	IEnumerator HitGround() { 
		yield return new WaitForSeconds(2.1f);
		// Place unit here
		print ("hit me baby one more time");

		gameObject.SetActive(false);
	}

	void OnEnable() {
		GroundIndicator.SetActive(false);
	  	Line.gameObject.SetActive(false);
	}

	void OnDisable() {
		GroundIndicator.SetActive(false);
		Line.gameObject.SetActive(false);
	}
}
