using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour, ILookable {
	#region ILookable implementation
	public void StartLooking (GameObject looker)
	{
		var dest = Vector3.Lerp(_startPos, looker.transform.position, 0.33f);
		iTween.MoveTo(gameObject, dest, 0.5f);
	}
	public void Looking (GameObject looker)
	{
//		throw new System.NotImplementedException ();
	}
	public void Activate (GameObject looker)
	{
//		throw new System.NotImplementedException ();
	}
	public void StopLooking (GameObject looker)
	{
		iTween.MoveTo(gameObject, _startPos, 0.5f);
	}
	public float TimeToActivate {
		get {
//			throw new System.NotImplementedException ();
			return 1f;
		}
	}
	#endregion

	private Vector3 _startPos;

	// Use this for initialization
	void Start () {
		_startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
