using UnityEngine;
using System.Collections;

public class UnitBehavior : MonoBehaviour {
	
	public float movementSpeed = 1;
	public Vector3 direction = Vector3.forward;

	enum States {moving, detected, attacking};
	public States state;

	// Use this for initialization
	void Start () {
		state = States.moving;
	}

	void Update(){
		Vector3 fwd = transform.TransformDirection(direction);
		if (Physics.Raycast(transform.position, fwd, 10))
			print("There is something in front of the object!");

		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
	}
}
