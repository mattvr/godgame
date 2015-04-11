using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {
	
	public float movementSpeed = 1;
	public Vector3 direction = Vector3.forward;
	
	public float detectRange = 5;
	public float attackRange = 1;

	public enum States {moving, detected, attacking};
	public States state;

	// Use this for initialization
	void Start () {
		state = States.moving;
	}

	void Update(){
		Vector3 fwd = transform.TransformDirection(direction);

		state = States.moving;
		if (Physics.Raycast (transform.position, fwd, detectRange)) {
			print ("I see you!");
			state = States.detected;
		}
		if (Physics.Raycast (transform.position, fwd, attackRange)) {
			print ("AARGH!");
			state = States.attacking;
		} else { // Only move if not attacking
			transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		}
		print (state);
	}
}
