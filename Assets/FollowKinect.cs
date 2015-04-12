using UnityEngine;
using System.Collections;

public class FollowKinect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var player = KinectStream.Instance.getPlayer();

		if (player != null)
			this.transform.localPosition = player.getJoint(11);
	}
}
