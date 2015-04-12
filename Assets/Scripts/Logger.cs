using UnityEngine;
using System.Collections;
using System.IO;

public class Logger : MonoBehaviour {

	public GameObject TrackedObject2;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (KinectStream.Instance.certainTracking ()) {
			Debug.Log("CERTAIN");
			//if (Input.GetKeyDown(KeyCode.Space)) {
			if (true) {
				//Debug.Log("LOGGING");
				string logline = "";
				logline += Vec2Str(KinectStream.Instance.getPlayer ().getJoint ("Head"));
				logline += "\t" + Vec2Str(OVRManager.display.GetHeadPose().position);
				Debug.Log(logline);
				using (var streamWriter = File.AppendText(@"log.txt")) {
					streamWriter.Write (logline + "\r\n");
				}
			}
		}
	}

	private string Vec2Str(Vector3 input) {
		string output = input.x.ToString("#.00000") + "," + input.y.ToString("#.00000") + "," + input.z.ToString("#.00000");
		return output;
	}
}
