using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkeletalFrame
{
	static readonly Dictionary<string,int> _JointMap = new Dictionary<string,int> {
		{"SpineBase",0},
		{"SpineMid",1},
		{"Neck",2},
		{"Head",3},
		{"ShoulderLeft",4},
		{"ElbowLeft",5},
		{"WristLeft",6},
		{"HandLeft",7},
		{"ShoulderRight",8},
		{"ElbowRight",9},
		{"WristRight",10},
		{"HandRight",11},
		{"HipLeft",12},
		{"KneeLeft",13},
		{"AnkleLeft",14},
		{"FootLeft",15},
		{"HipRight",16},
		{"KneeRight",17},
		{"AnkleRight",18},
		{"FootRight",19},
		{"SpineShoulder",20},
		{"HandTipLeft",21},
		{"ThumbLeft",22},
		{"HandTipRight",23},
		{"ThumbRight",24}
	};
	public Vector3[] joints;
	public long bodyID;

	public SkeletalFrame (long bodyIDin)
	{
		joints = new Vector3[25];
		for (int i=0; i<25; i++) {
			joints [i] = Vector3.zero;
		}
		bodyID = bodyIDin;
	}

	public Vector3 getJoint (int jointID)
	{
		var vec = joints [jointID];
		var transformed = new Vector3 (vec.x, vec.y, vec.z);
		return transformed;
	}

	public Vector3 getJoint (string jointName)
	{
		return joints [_JointMap [jointName]];
	}
}

public class KinectStream : MonoBehaviour
{
	public bool IsRightPlayer;
	int currentPlayer = -1;
	long lastPlayerID;
	public static KinectStream Instance;
	public List<SkeletalFrame> data;
	const string kinectHTTP = "http://localhost:1234";
	static readonly Dictionary<string,int> _JointMap = new Dictionary<string,int> {
		{"SpineBase",0},
		{"SpineMid",1},
		{"Neck",2},
		{"Head",3},
		{"ShoulderLeft",4},
		{"ElbowLeft",5},
		{"WristLeft",6},
		{"HandLeft",7},
		{"ShoulderRight",8},
		{"ElbowRight",9},
		{"WristRight",10},
		{"HandRight",11},
		{"HipLeft",12},
		{"KneeLeft",13},
		{"AnkleLeft",14},
		{"FootLeft",15},
		{"HipRight",16},
		{"KneeRight",17},
		{"AnkleRight",18},
		{"FootRight",19},
		{"SpineShoulder",20},
		{"HandTipLeft",21},
		{"ThumbLeft",22},
		{"HandTipRight",23},
		{"ThumbRight",24}
	};
	WWW lastRequest;
	// Use this for initialization
	void Start ()
	{
		Instance = this;
		data = new List<SkeletalFrame> ();
		lastRequest = new WWW (kinectHTTP);
	}

	// Update is called once per frame
	void Update ()
	{
		if (lastRequest.isDone) {
			if (string.IsNullOrEmpty (lastRequest.error)) {
				string frame = lastRequest.text;
				processFrame (frame);
			} else {
				Debug.Log (lastRequest.error);
			}
			lastRequest = new WWW (kinectHTTP);
		}

	}

	Vector3 parseVector3 (string inString)
	{
		string[] xyz = inString.Split (new char[] {','});
		return new Vector3 (float.Parse (xyz [0]), float.Parse (xyz [1]), float.Parse (xyz [2]));
	}

	void processFrame (string frame)
	{
		List<SkeletalFrame> newdata = new List<SkeletalFrame> ();
		string[] lines = frame.Split (new char[] { '\n' });
		SkeletalFrame current = null;
		for (int i=0; i<lines.Length; i++) {
			string[] words = lines [i].Split (new char[] {' '});
			if (words.Length != 2) {
				continue;
			}
			if (words [0].Contains ("ID")) {
				if (current != null) {
					newdata.Add (current);
				}
				current = new SkeletalFrame (long.Parse (words [1]));
			} else if (current != null) {
				if (_JointMap.ContainsKey (words [0])) {
					int jointIndex = _JointMap [words [0]];
					current.joints [jointIndex] = parseVector3 (words [1]);
				}
			}
		}
		newdata.Add (current);

		data = newdata;

		if (data.Count > 0) {
			if (data.Count == 1) {
				if (IsRightPlayer) {// one player, this player
					lastPlayerID = data[0].bodyID;
					currentPlayer = 0;
				} else {//one player, not this player
					lastPlayerID = 41;//random bad id
					currentPlayer = -1;
				}
			} else if (data.Count == 2) {//two players
				if (IsRightPlayer) {//pick the right player out of two
					if (data[0].getJoint("Head").x > data[1].getJoint("Head").x) {//if its to the right
						lastPlayerID = data[0].bodyID;
						currentPlayer = 0;
					} else {
						lastPlayerID = data[1].bodyID;
						currentPlayer = 1;
					}
				} else {//pick the left player out of two
					if (data[0].getJoint("Head").x < data[1].getJoint("Head").x) {//if its to the left
						lastPlayerID = data[0].bodyID;
						currentPlayer = 0;
					} else {
						lastPlayerID = data[1].bodyID;
						currentPlayer = 1;
					}
				}
			} else {// more than two players
				bool shortCircuit = false;
				for (int i = 0; i < data.Count; i++) {
					if (data[i].bodyID == lastPlayerID) {
						//lastplayerid stays the same
						currentPlayer = i;
						shortCircuit = true;
					}
				}
				if (!shortCircuit) {
					int extremeBody = 0;//index of right most or left most body
					float xMost;//x value of right most or left most body
					if (IsRightPlayer) {
						xMost = -9999f;
					} else {
						xMost = 9999f;
					}
					for (int i = 0; i < data.Count; i++) {//go through all bodies, pick x most
						if (IsRightPlayer) {
							if (data[i].getJoint("Head").x > xMost) {
								extremeBody = i;
								xMost = data[i].getJoint("Head").x;
							}
						} else {
							if (data[i].getJoint("Head").x < xMost) {
								extremeBody = i;
								xMost = data[i].getJoint("Head").x;
							}
						}
					}
					lastPlayerID = data[extremeBody].bodyID;
					currentPlayer = extremeBody;
				}
			}
		} else {
			currentPlayer = -1;
		}

	}

	public SkeletalFrame getPlayer ()
	{
		if (currentPlayer == -1) {
			return null;
		} else {
			return data[currentPlayer];
		}
	}

	public bool certainTracking()
	{
		return data.Count == 1;
	}
}
