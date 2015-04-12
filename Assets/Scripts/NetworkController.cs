using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour {
	
	int ID;
	public bool IsRightPlayer;
	
	void Awake() {
		Application.runInBackground = true;
	}
	
	// Use this for initialization
	void Start () {
		IsRightPlayer = this.GetComponent<KinectStream>().IsRightPlayer;
		
		if (IsRightPlayer)
			Network.InitializeServer(10, 1235, true);
		else
			Network.Connect("169.254.158.26", 1235);
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Started server");
		SpawnPlayer();
	}
	
	void OnConnectedToServer() {
		Debug.Log ("Connected to server");
		Camera.main.transform.parent.position = new Vector3(0, 0, 2.4f);
		Camera.main.transform.parent.Rotate(new Vector3(0, 180, 0));
		SpawnPlayer();
		
	}
	
	void SpawnPlayer() {
		var go = Network.Instantiate(R.GetCachedResource("Prefabs/Player"), Vector3.zero, Quaternion.identity, 0) as GameObject;
		var ply = go.GetComponent<Player>();
		if (IsRightPlayer) {
			ply.Self = true;
			var sc = ply.transform.localScale;
			var pos = ply.transform.position;
			ply.transform.parent = Camera.main.transform;
			ply.transform.localScale = sc;
			ply.transform.position = ply.transform.position;
		}
		ply.Init();
		
		
		
	}
}
