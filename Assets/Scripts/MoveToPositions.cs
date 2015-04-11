using UnityEngine;
using System;
using System.Collections.Generic;

public class MoveToPositions : MonoBehaviour {

	public GameObject[] CameraPositions;
	private List<KeyCode> InputList;

	// Use this for initialization
	void Start () {
		InputList = new List<KeyCode>();
		for (int i = 0; i < CameraPositions.Length; i++) {
			var e = (KeyCode) Enum.Parse(typeof(KeyCode), "Alpha" + (i + 1));
			InputList.Add(e);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < InputList.Count; i++) {
			if (Input.GetKeyDown(InputList[i])) {
				this.transform.position = CameraPositions[i].transform.position;
			}
		}
	}
}
