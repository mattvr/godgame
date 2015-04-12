using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour, ILookable {
	public enum Type {
		Unit,
		Building,
		Power
	}

	public Type CardType;
	public CardHand Hand;
	public bool Selected = false;
	
	private Vector3 defaultPos;

	#region ILookable implementation
	public void StartLooking (GameObject looker)
	{
		var dest = Vector3.Lerp(this.transform.position, looker.transform.position, 0.33f);
		iTween.MoveTo(gameObject, dest, 0.5f);
		Selected = true;
	}
	public void Looking (GameObject looker)
	{
	}
	public object Activate (GameObject looker)
	{
		Hand.PlayCard(this);
		return null;
	}
	public void StopLooking (GameObject looker)
	{
		iTween.MoveTo(gameObject, iTween.Hash ("position", Vector3.zero, "islocal", true, "time", 0.5f));
		Selected = false;
	}

	public float TimeToActivate {
		get {
			return 2.5f;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		defaultPos = this.transform.parent.position;
	}
	
	public void MovePos(Vector3 pos) {
		defaultPos = pos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Remove() {
		iTween.Stop(this.transform.parent.gameObject);
		iTween.Stop(this.gameObject);
	}
}
