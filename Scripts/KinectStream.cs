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
				return joints [jointID];
		}

		public Vector3 getJoint (string jointName)
		{
				return joints [_JointMap [jointName]];
		}
}

public class KinectStream : MonoBehaviour
{

		public List<SkeletalFrame> data;
		public string kinectHTTP = "http://10.113.4.63:1234";
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
						}
						lastRequest = new WWW (kinectHTTP);
				}
				if (data.Count > 0) {
						Debug.Log (data [0].getJoint (0));
				}
		}

		Vector3 parseVector3 (string inString)
		{
				string[] xyz = inString.Split (new char[] {','});
				return new Vector3 (float.Parse (xyz [0]), float.Parse (xyz [1]), float.Parse (xyz [2]));
		}

		void processFrame (string frame)
		{
				Debug.Log (frame);
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
		}
}