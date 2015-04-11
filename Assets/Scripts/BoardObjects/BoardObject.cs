using UnityEngine;
using System.Collections;

public abstract class BoardObject {

	bool mortal;

	float toughness; // 0-1.0 for strong you are
	float damage; 	 // 0-1.0 for how much you take away from the toughness of who you're attacking

	public void attack(BoardObject opponent) {
		// imagine my damage is 0.1 and the opponents toughness is 0.9

		float attack = Random.Range (0.0F, 1.0F) + damage;

		if (opponent.toughness < attack) {
			opponent.die ();
		}
	}
	
	public void die() {

	}
}
