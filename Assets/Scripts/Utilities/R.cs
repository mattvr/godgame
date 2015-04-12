﻿using UnityEngine;
using System.Collections.Generic;

public class R : MonoBehaviour {
	
	private static R _instance;
	private static Dictionary<string, Object> _resources;
	private static List<Object> Cards;
	
	void Awake () {
		_instance = this;
		Cards = new List<Object>();

		foreach(var c in Resources.LoadAll("Prefabs/Cards")) {
			Cards.Add(c);
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static Object GetCachedResource(string path) {
		if (_resources == null) {
			_resources = new Dictionary<string, Object>();
		}
		else if (_resources.ContainsKey(path)) {
			return _resources[path];
		}
		
		var res = Resources.Load(path);
		_resources.Add(path, res);
		return res;
	}

	public static Object GetRandomCard() {
		return Cards[Random.Range(0, Cards.Count)];
	}
}