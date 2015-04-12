using UnityEngine;
using System.Collections.Generic;
using System;

public class CardHand : MonoBehaviour {

	private List<Card> Cards;
	private float drawTime = 5;
	private float lastDraw = 0;
	private const float spread = 2f;
	private float curSpread;
	private Vector3 defPos;

	// Use this for initialization
	void Start () {
		Cards = new List<Card>();
		InvokeRepeating("DrawCard", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DrawCard() {
		if (Cards.Count >= 10) return;

		// Create a random card
		var obj = R.GetRandomCard();
		var go = Network.Instantiate(obj, Vector3.zero, Quaternion.identity, 0) as GameObject;

		// Intialize object
		var sc = go.transform.localScale;
		var c = go.GetComponentInChildren<Card>();
		c.CardName = obj.name;
		go.transform.parent = this.transform;
//		go.transform.localPosition = Vector3.zero;
		c.MovePos(go.transform.parent.position);
		go.transform.localPosition = new Vector3(0, -0.5f, 0);
		go.transform.localScale = sc;

		// Add to hand
		c.Hand = this;
		Cards.Add(c);
		FanOutCards();
	}

	void FanOutCards() {
//		curSpread = spread + (Cards.Count * 0.1f);

		for (int i = 0; i < Cards.Count; i++) {
			float zPos = (((float) (i + 1) / (Cards.Count + 1)) * spread) - (spread / 2f);
			
			var parent = Cards[i].transform.parent.gameObject;
			var prevPos = parent.transform.localPosition;
			var newPos = new Vector3(i * 0.01f, 0, zPos);
			parent.transform.localPosition = newPos;
			Cards[i].GetComponentInChildren<Card>().MovePos(parent.transform.position);
			parent.transform.localPosition = prevPos;

			// Don't move highlighted cards.
			if (Cards[i].Selected) continue;

			iTween.MoveTo(parent, iTween.Hash ("position", newPos, "time", 1f, "islocal", true));
		}
	}

	public void PlayCard(Card c) {
		Cards.Remove(c);
		c.Remove();
		HandSphere.Instance.ThingToSpawn = c.CardType + "s/" + c.name;
		Network.Destroy (c.transform.parent.gameObject);
	}
}
