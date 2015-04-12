using UnityEngine;
using System.Collections;

public class Logger : MonoBehaviour {

	public GameObject TrackedObject2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (KinectStream.Instance.data.Count != 1)
			return;
		Debug.Log (KinectStream.Instance.getPlayer ().getJoint ("Head").ToString() + ":" + TrackedObject2.transform.position.ToString() + ":" + (TrackedObject2.transform.position -this.gameObject.transform.position).ToString());
	}
}
