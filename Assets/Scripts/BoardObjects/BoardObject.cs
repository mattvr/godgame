using UnityEngine;
using System.Collections;

public class BoardObject : MonoBehaviour {

	public bool aggressive; 	// Do I go places and slay things?
	public float toughness;  	// 0-1.0 for how strong you are

	public float movementSpeed = 0;
	public Vector3 direction = Vector3.back; 
	
	public float detectRange = 5;
	public float attackRange = 1;
	
	public enum States {moving, detected, attacking, stopped};
	public States state;

	IEnumerator attacking;
	
	// Use this for initialization
	void Start () {}
	
	void Update(){
		RaycastHit hit = new RaycastHit();
		state = States.moving;
		
		if (Physics.Raycast (transform.position, direction, detectRange)) {
			state = States.detected;
			print (state);
		}
		if (Physics.Raycast (transform.position, direction, out hit, attackRange)) {
			state = States.attacking;
			print (state);
			BoardObject opponent = (BoardObject) hit.collider.gameObject.GetComponent("BoardObject");

			// in range
			// start attacking the enemy
			attacking = Attack (opponent);
			StartCoroutine(attacking); // repeatedly calls Attack on target
		} else { // Only move if not attacking
			if (this.aggressive) {
				transform.Translate(direction * movementSpeed * Time.deltaTime);

			}
		}
	}

	IEnumerator Attack(BoardObject opponent) {
		while (opponent) {
			yield return new WaitForSeconds(1f); // each loop, wait for 1 second before continuing
	
			float attack = Random.Range (0.0F, 1.0F);
			
			if (opponent.toughness < attack) {
				opponent.die ();
				StopCoroutine(attacking);
			}
		}
		// done
	}
	
	public void die() {
		print ("I'm dead!");
		Destroy (gameObject);
	}
}
