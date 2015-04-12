using UnityEngine;
using System.Collections;

public class Logger : MonoBehaviour {

	public GameObject TrackedObject2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (KinectStream.Instance.certainTracking ()) {
			Debug.Log("CERTAIN");
			if (Input.GetKeyDown(KeyCode.Space)) {
				Debug.Log("LOGGING");
				string logline = "";
				KinectStream.Instance.getPlayer ().getJoint ("Head").ToString ();
			}
			Debug.Log ( + ":" + TrackedObject2.transform.position.ToString () + ":" + (TrackedObject2.transform.position - this.gameObject.transform.position).ToString ());
		}
	}
}
