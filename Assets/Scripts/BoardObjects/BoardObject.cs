using UnityEngine;
using System.Collections;

public class BoardObject : MonoBehaviour {

	public bool destructible;	// Can I be destroyed?
	public bool active; 		// Do I go places and slay things?

	public float toughness;  // 0-1.0 for strong you are
	public float damage; 	 // 0-1.0 for how much you take away from the toughness of who you're attacking

	public float movementSpeed = 2;
	public Vector3 direction = Vector3.forward;
	
	public float detectRange = 5;
	public float attackRange = 1;
	
	public enum States {moving, detected, attacking, stopped};
	public States state;
	
	// Use this for initialization
	void Start () {
		this.destructible = true;
		this.state = States.moving;
		this.toughness = 1.0F;
		this.damage = 1.0F;
	}
	
	void Update(){
		Vector3 fwd = transform.TransformDirection(direction);
		RaycastHit hit = new RaycastHit();
		state = States.moving;
		
		if (Physics.Raycast (transform.position, fwd, detectRange)) {
			state = States.detected;
		}
		if (Physics.Raycast (transform.position, fwd, out hit, attackRange)) {
			state = States.attacking;
			BoardObject opponent = (BoardObject) hit.collider.gameObject.GetComponent("BoardObject");
			this.attack(opponent);
		} else { // Only move if not attacking
			transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		}
		print (state);
	}

	public void attack(BoardObject opponent) {
		// imagine my damage is 0.1 and the opponents toughness is 0.9
		float attack = Random.Range (0.0F, 1.0F) + damage;
		print (attack);
		if (opponent.toughness < attack) {
			opponent.die ();
		}
	}
	
	public void die() {
		print ("I'm dayin!");
		if (this.destructible) {
			Destroy (gameObject);
		}
	}
}
