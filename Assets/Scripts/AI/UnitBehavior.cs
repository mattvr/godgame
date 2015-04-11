using UnityEngine;
using System.Collections;

public class UnitBehavior : MonoBehaviour {
	
	public float movementSpeed = 1;
	public Vector3 direction = Vector3.forward;

	public enum States {moving, detected, attacking};
	public States state;

	// Use this for initialization
	void Start () {
		state = States.moving;
	}

	void Update(){
		Vector3 fwd = transform.TransformDirection(direction);

		state = States.moving;
		if (Physics.Raycast (transform.position, fwd, 5)) {
			print ("I see you!");
			state = States.detected;
		}
		if (Physics.Raycast (transform.position, fwd, 1)) {
			print ("AARGH!");
			state = States.attacking;
		} 

		print (state);
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
	}
}
